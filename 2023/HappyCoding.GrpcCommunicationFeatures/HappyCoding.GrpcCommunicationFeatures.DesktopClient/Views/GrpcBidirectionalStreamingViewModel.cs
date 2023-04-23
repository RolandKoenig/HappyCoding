using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grpc.Core;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using Microsoft.Extensions.Logging;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient.Views;

public partial class GrpcBidirectionalStreamingViewModel : ObservableObject
{
    private readonly ILogger _logger;
    private readonly BidirectionalEventStreamService.BidirectionalEventStreamServiceClient _streamCreatorClient;

    [ObservableProperty]
    private ObservableCollection<string> _receivedEvents = new ObservableCollection<string>();

    private AsyncDuplexStreamingCall<StreamRequest, StreamReply>? _currentRequest;

    [ObservableProperty]
    private bool _isCurrentStreamStarted = false;

    [ObservableProperty]
    private bool _isCurrentStreamStopped = false;

    public static GrpcBidirectionalStreamingViewModel DesignTimeViewModel => new (null!, null!);

    public GrpcBidirectionalStreamingViewModel(
        ILogger<GrpcServerSideStreamingViewModel> logger,
        BidirectionalEventStreamService.BidirectionalEventStreamServiceClient streamCreatorClient)
    {
        _logger = logger;
        _streamCreatorClient = streamCreatorClient;
        
        this.UpdateObservableProperties();
    }

    private async void SetCurrentStream(AsyncDuplexStreamingCall<StreamRequest, StreamReply>? request)
    {
        this.UpdateObservableProperties();
        if ((_currentRequest == null) ||
            (_currentRequest != request))
        {
            return;
        }

        var stream = request.ResponseStream.ReadAllAsync();
        try
        {
            await foreach (var actEvent in stream)
            {
                if (_currentRequest != request) { break; }

                Guid actGuid;
                DateTimeOffset actTimestamp;
                string eventContent;
                try
                {
                    actGuid = Guid.Parse(actEvent.EventGuid);
                    actTimestamp = actEvent.Timestamp.ToDateTimeOffset();
                    eventContent = actEvent.EventContent;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to process event from server");
                    continue;
                }

                _receivedEvents.Add(
                    $"[{actTimestamp}] Received event {actGuid}: {eventContent}");
            }
        }
        catch (Exception exOuter)
        {
            _logger.LogError(exOuter, "Unable to get events from server. Stopping the stream now...");
        }

        await request.RequestStream.CompleteAsync();

        await Task.Delay(1000); // Wait some time before disposing the stream
        request.Dispose();
        
        if (_currentRequest == request)
        {
            _currentRequest = null;
            this.UpdateObservableProperties();
        }
    }
    
    [RelayCommand]
    public async void StartStreaming()
    {
        try
        {
            var request = new StreamRequest()
            {
                EventName = GenerateEventName()
            };

            _currentRequest = _streamCreatorClient.OpenEventStream();
            
            await _currentRequest.RequestStream.WriteAsync(request);
            
            SetCurrentStream(_currentRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while opening stream");
            _currentRequest = null;
        }
    }

    [RelayCommand]
    public void StopStreaming()
    {
        _currentRequest = null;
        
        this.UpdateObservableProperties();
    }

    [RelayCommand]
    public async void ChangeEvent()
    {
        var currentRequest = _currentRequest;
        if (currentRequest != null)
        {
            var request = new StreamRequest()
            {
                EventName = GenerateEventName()
            };

            try
            {
                await currentRequest.RequestStream.WriteAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing event");
            }
        }
    }

    [RelayCommand]
    public void ClearEventLog()
    {
        this.ReceivedEvents.Clear();
    }

    private void UpdateObservableProperties()
    {
        this.IsCurrentStreamStarted = _currentRequest != null;
        this.IsCurrentStreamStopped = _currentRequest == null;
    }

    private static string GenerateEventName()
    {
        var randomizer = new Random(Environment.TickCount);
        return $"Event{randomizer.Next(1, 1000)}";
    }
}
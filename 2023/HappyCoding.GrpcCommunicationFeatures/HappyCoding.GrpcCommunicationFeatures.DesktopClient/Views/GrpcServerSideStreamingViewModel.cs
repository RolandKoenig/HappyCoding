using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grpc.Core;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using Microsoft.Extensions.Logging;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient.Views;

public partial class GrpcServerSideStreamingViewModel : ObservableObject
{
    private readonly ILogger _logger;
    private readonly EventStreamService.EventStreamServiceClient _streamCreatorClient;

    [ObservableProperty]
    private ObservableCollection<string> _receivedEvents = new ObservableCollection<string>();

    private IAsyncEnumerable<StreamReply>? _currentStream;

    [ObservableProperty]
    private bool _isCurrentStreamStarted = false;

    [ObservableProperty]
    private bool _isCurrentStreamStopped = false;

    public static GrpcServerSideStreamingViewModel DesignTimeViewModel => new (null!, null!);

    public GrpcServerSideStreamingViewModel(
        ILogger<GrpcServerSideStreamingViewModel> logger,
        EventStreamService.EventStreamServiceClient streamCreatorClient)
    {
        _logger = logger;
        _streamCreatorClient = streamCreatorClient;
        
        this.UpdateObservableProperties();
    }

    private async void SetCurrentStream(IAsyncEnumerable<StreamReply>? stream, IDisposable streamSource)
    {
        _currentStream = stream;
        this.UpdateObservableProperties();
        if (_currentStream == null)
        {
            return;
        }

        try
        {
            await foreach (var actEvent in _currentStream)
            {
                if (_currentStream != stream) { break; }

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
        
        streamSource.Dispose();
        if (_currentStream == streamSource)
        {
            _currentStream = null;
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
                EventName = "MyTestEvent"
            };

            var response = _streamCreatorClient.OpenEventStream(request);
            _currentStream = response.ResponseStream.ReadAllAsync();
            
            SetCurrentStream(_currentStream, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while opening stream");
        }
    }

    [RelayCommand]
    public void StopStreaming()
    {
        _currentStream = null;
        
        this.UpdateObservableProperties();
    }

    [RelayCommand]
    public void ClearEventLog()
    {
        this.ReceivedEvents.Clear();
    }

    private void UpdateObservableProperties()
    {
        this.IsCurrentStreamStarted = _currentStream != null;
        this.IsCurrentStreamStopped = _currentStream == null;
    }
}
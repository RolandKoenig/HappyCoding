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

    private ObservableCollection<string> _receivedEvents = new ObservableCollection<string>();

    private IAsyncEnumerable<StreamReply>? _currentStream;

    public GrpcServerSideStreamingViewModel(
        ILogger<GrpcServerSideStreamingViewModel> logger,
        EventStreamService.EventStreamServiceClient streamCreatorClient)
    {
        _logger = logger;
        _streamCreatorClient = streamCreatorClient;
    }

    private async void SetCurrentStream(IAsyncEnumerable<StreamReply>? stream, IDisposable streamSource)
    {
        _currentStream = stream;
        if (_currentStream == null) { return; }
        
        await foreach (var actEvent in _currentStream)
        {
            if (_currentStream != stream) { break; }

            Guid actGuid;
            DateTimeOffset actTimestamp;
            string eventContent;
            try
            {
                actGuid = Guid.Parse(actEvent.EventGuid);
                actTimestamp = new DateTimeOffset(
                    actEvent.Timestamp.TimestampTicks,
                    TimeSpan.FromTicks(actEvent.Timestamp.OffsetTicks));
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
        
        streamSource.Dispose();
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
            _logger.LogError(ex, "Error while calling Greeter.SayHello");
        }
    }

    public void StopStreaming()
    {
        _currentStream = null;
    }
}
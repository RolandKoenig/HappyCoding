using Grpc.Core;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;

namespace HappyCoding.GrpcCommunicationFeatures.GrpcServices;

public class BidirectionalStreamingService : BidirectionalEventStreamService.BidirectionalEventStreamServiceBase
{
    /// <inheritdoc />
    public override async Task OpenEventStream(IAsyncStreamReader<StreamRequest> requestStream, IServerStreamWriter<StreamReply> responseStream, ServerCallContext context)
    {
        var currentEvent = "Unknown";
        var counter = 0;
        
        using var requestStreamSubscription = requestStream
            .ReadAllAsync()
            .ToObservable()
            .Subscribe(
                incomingRequest =>
                {
                    currentEvent = incomingRequest.EventName;
                    counter = 0;
                },
                _ => { });

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
            if (context.CancellationToken.IsCancellationRequested) { break;}

            var newGuid = Guid.NewGuid();
            var currentTimestamp = DateTimeOffset.UtcNow;
            
            counter++;
            await responseStream.WriteAsync(new StreamReply()
            {
                EventGuid = newGuid.ToString(),
                EventContent = $"Reply for event {currentEvent} #{counter}",
                Timestamp = new Timestamp()
                {
                    TimestampTicks = currentTimestamp.Ticks,
                    OffsetTicks = currentTimestamp.Offset.Ticks
                }
            });
        }
    }
}
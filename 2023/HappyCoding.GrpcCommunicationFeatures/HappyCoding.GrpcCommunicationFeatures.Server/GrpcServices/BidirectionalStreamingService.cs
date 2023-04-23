using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Utils;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;

namespace HappyCoding.GrpcCommunicationFeatures.Server.GrpcServices;

public class BidirectionalStreamingService : BidirectionalEventStreamService.BidirectionalEventStreamServiceBase
{
    /// <inheritdoc />
    public override async Task OpenEventStream(IAsyncStreamReader<StreamRequest> requestStream, IServerStreamWriter<StreamReply> responseStream, ServerCallContext context)
    {
        var currentEvent = "Unknown";
        var counter = 0;

        // Listen for incoming updates
        var requestStreamSubscription = requestStream
            .ForEachAsync(incomingRequest =>
            {
                currentEvent = incomingRequest.EventName;
                counter = 0;

                return Task.CompletedTask;
            });

        // Generate events for the client
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
                Timestamp = Timestamp.FromDateTimeOffset(currentTimestamp)
            });
        }
    }
}
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;

namespace HappyCoding.GrpcCommunicationFeatures.Server.GrpcServices;

public class ServerSideStreamingService : EventStreamService.EventStreamServiceBase
{
    /// <inheritdoc />
    public override async Task OpenEventStream(StreamRequest request, IServerStreamWriter<StreamReply> responseStream, ServerCallContext context)
    {
        var counter = 0;
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
                EventContent = $"Reply #{counter}",
                Timestamp = Timestamp.FromDateTimeOffset(currentTimestamp)
            });
        }
    }
    
}
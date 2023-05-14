using Grpc.Core;
using HappyCoding.GrpcWithNet48.ProtoDefinitions;

namespace HappyCoding.GrpcWithNet48.AspNetCoreServer.Services;

public class ServerSideStreamingService : ServerSideStreaming.ServerSideStreamingBase
{
    public override async Task OpenStream(
        OpenStreamRequest request,
        IServerStreamWriter<StreamReply> responseStream,
        ServerCallContext context)
    {
        await Task.Delay(500);

        for (var loop = 0; loop < 5; loop++)
        {
            await responseStream.WriteAsync(
                new StreamReply()
                {
                    Message = $"Stream reply #{loop + 1} for event {request.EventName}"
                }, 
                context.CancellationToken);
            await Task.Delay(500);
        }
    }
}

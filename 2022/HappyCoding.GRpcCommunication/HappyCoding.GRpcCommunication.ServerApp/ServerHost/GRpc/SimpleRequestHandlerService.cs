using System;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.SimpleRequest;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.GRpc;

public class SimpleRequestHandlerService : SimpleRequestHandler.SimpleRequestHandlerBase
{
    private readonly ServerOptions _options;

    public SimpleRequestHandlerService(ServerOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public override async Task<SimpleResponse> Handle(SimpleRequest request, ServerCallContext context)
    {
        if (_options.SimulatedProcessingTimeMS > 0)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(_options.SimulatedProcessingTimeMS));
        }

        return new SimpleResponse()
        {
            Message = "Test message"
        };
    }
}

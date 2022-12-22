using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.SimpleRequest;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.GRpc;

public class SimpleRequestHandlerService : SimpleRequestHandler.SimpleRequestHandlerBase
{
    private readonly IFirLibMessagePublisher _messagePublisher;

    public SimpleRequestHandlerService(IFirLibMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    /// <inheritdoc />
    public override Task<SimpleResponse> Handle(SimpleRequest request, ServerCallContext context)
    {
        return Task.FromResult(new SimpleResponse()
        {
            Message = "Test message"
        });
    }
}

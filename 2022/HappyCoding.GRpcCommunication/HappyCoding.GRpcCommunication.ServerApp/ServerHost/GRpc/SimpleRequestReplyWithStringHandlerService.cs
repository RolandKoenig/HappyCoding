using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.Services;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.GRpc;

public class SimpleRequestReplyWithStringHandlerService : SimpleRequestReplyWithStringHandler.SimpleRequestReplyWithStringHandlerBase
{
    private readonly IFirLibMessagePublisher _messagePublisher;

    public SimpleRequestReplyWithStringHandlerService(IFirLibMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    /// <inheritdoc />
    public override Task<SimpleReplyWithString> Handle(SimpleRequestWithString request, ServerCallContext context)
    {
        return base.Handle(request, context);
    }
}

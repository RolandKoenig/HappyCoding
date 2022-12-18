using HappyCoding.GRpcCommunication.ServerApp.ServerHost;
using HappyCoding.GRpcCommunication.ServerApp.Views;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.Messages;

[FirLibMessage]
[MessagePossibleSource(ServerConstants.SERVER_MESSENGER_NAME)]
[MessageAsyncRoutingTargets(ViewConstants.VIEW_MESSENGER_NAME)]
public record ServerStateChangedMessage(bool IsRunning);
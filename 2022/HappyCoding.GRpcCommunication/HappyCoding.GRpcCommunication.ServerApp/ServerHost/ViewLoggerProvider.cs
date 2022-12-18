using Microsoft.Extensions.Logging;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost;

internal class ViewLoggerProvider : ILoggerProvider
{
    private readonly IFirLibMessagePublisher _messagePublisher;

    public ViewLoggerProvider(IFirLibMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        
    }

    /// <inheritdoc />
    public ILogger CreateLogger(string categoryName)
    {
        return new ViewLogger(_messagePublisher);
    }
}

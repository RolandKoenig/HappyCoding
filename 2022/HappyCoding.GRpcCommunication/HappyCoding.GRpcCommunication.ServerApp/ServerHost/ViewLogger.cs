using System;
using HappyCoding.GRpcCommunication.ServerApp.Messages;
using Microsoft.Extensions.Logging;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost;

internal class ViewLogger : ILogger
{
    private readonly IFirLibMessagePublisher _messagePublisher;

    public ViewLogger(IFirLibMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    /// <inheritdoc />
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    /// <inheritdoc />
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _messagePublisher.Publish(new ServerLogReceivedMessage(
            DateTimeOffset.UtcNow, 
            logLevel, 
            formatter(state, exception)));
    }
}

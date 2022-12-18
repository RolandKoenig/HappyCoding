using System;
using Microsoft.Extensions.Logging;

namespace HappyCoding.GRpcCommunication.ServerApp.Views;

public class ServerLogEvent
{
    public DateTimeOffset Timestamp { get; }

    public string TimestampStr => this.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff");

    public LogLevel LogLevel { get; }

    public string Message { get; }

    public ServerLogEvent(DateTimeOffset timestamp, LogLevel logLevel, string message)
    {
        this.Timestamp = timestamp;
        this.LogLevel = logLevel;
        this.Message = message;
    }
}
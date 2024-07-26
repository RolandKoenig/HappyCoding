using System;
using NServiceBus;

namespace HappyCoding.NServiceBusWithSqlServer.Common
{
    public class TestMessage : ICommand
    {
        public DateTimeOffset SendTimestamp { get; set; } = DateTimeOffset.UtcNow;

        public string MessageContent { get; set; } = string.Empty;
    }
}

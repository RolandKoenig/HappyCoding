using System;
using System.Threading.Tasks;
using HappyCoding.NServiceBusWithSqlServer.Common;
using NServiceBus;

namespace HappyCoding.NServiceBusWithSqlServer.ReceiverApp
{
    public class TestMessageHandler : IHandleMessages<TestMessage>
    {
        public Task Handle(TestMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Received message: Timestamp={message.SendTimestamp}, Message={message.MessageContent}");

            throw new TestException();
        }
    }
}

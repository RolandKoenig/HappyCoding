using System;
using System.Threading;
using System.Threading.Tasks;
using HappyCoding.NServiceBusWithSqlServer.Common;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace HappyCoding.NServiceBusWithSqlServer.SenderApp
{
    internal class HostedService : BackgroundService
    {
        private readonly IMessageSession _messageSession;

        public HostedService(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("SenderApp started..");

            var msgCounter = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(1000, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                var message = new TestMessage()
                {
                    MessageContent = $"Test message #{msgCounter}"
                };
                await _messageSession.Send(message);
                Console.WriteLine($"Message sent: Timestamp={message.SendTimestamp}, Message={message.MessageContent}");

                msgCounter++;
            }
        }
    }
}

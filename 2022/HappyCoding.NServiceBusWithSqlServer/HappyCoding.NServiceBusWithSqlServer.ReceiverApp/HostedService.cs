using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HappyCoding.NServiceBusWithSqlServer.ReceiverApp
{
    internal class HostedService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
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
            }
        }
    }
}

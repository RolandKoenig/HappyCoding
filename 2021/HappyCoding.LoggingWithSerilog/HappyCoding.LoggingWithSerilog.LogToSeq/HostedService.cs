using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HappyCoding.LoggingWithSerilog.LogToSeq
{
    internal sealed class HostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        public HostedService(
            ILogger<HostedService> logger,
            IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    var count = 0;
                    while (true)
                    {
                        count++;

                        if (count % 7 == 0)
                        {
                            _logger.LogDebug("Dummy-Logging #{Counter}", count);
                        }
                        else if (count % 6 == 0)
                        {
                            _logger.LogWarning("Dummy-Logging #{Counter}", count);
                        }
                        else if (count % 5 == 0)
                        {
                            _logger.LogError("Dummy-Logging #{Counter}", count);
                        }
                        else if (count % 4 == 0)
                        {
                            _logger.LogCritical("Dummy-Logging #{Counter}", count);
                        }
                        else
                        {
                            _logger.LogInformation("Dummy-Logging #{Counter}", count);
                        }

                        await Task.Delay(1000);
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

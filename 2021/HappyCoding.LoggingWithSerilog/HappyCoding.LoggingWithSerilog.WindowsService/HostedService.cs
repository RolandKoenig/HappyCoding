using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HappyCoding.LoggingWithSerilog.WindowsService
{
    internal sealed class HostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        private Task? _mainTask;
        private readonly CancellationTokenSource _mainTaskCancelTokenSource;

        public HostedService(
            ILogger<HostedService> logger,
            IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _mainTaskCancelTokenSource = new CancellationTokenSource();
        }

        private async Task ServiceMainMethodAsync()
        {
            // Startup logic
            _logger.LogInformation("Hosted service started");

            // Main loop logic
            var count = 0;
            while (!_mainTaskCancelTokenSource.IsCancellationRequested)
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

                try
                {
                    await Task.Delay(1000, _mainTaskCancelTokenSource.Token)
                        .ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    // Ignore this exception.. 
                }
            }

            // Shutdown logic
            _logger.LogInformation("Hosted service stopped");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                _mainTask = Task.Run(
                    this.ServiceMainMethodAsync, 
                    cancellationToken);
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _mainTaskCancelTokenSource.Cancel();

            var mainTask = _mainTask;
            if (mainTask != null)
            {
                return mainTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}

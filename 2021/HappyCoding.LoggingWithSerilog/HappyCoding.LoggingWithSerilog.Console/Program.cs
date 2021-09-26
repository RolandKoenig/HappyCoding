using System;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole.Themes;

namespace HappyCoding.LoggingWithSerilog.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("./log/log-.txt", rollingInterval: RollingInterval.Minute, retainedFileCountLimit:3)
                .CreateLogger();

            var count = 0;
            while (true)
            {
                count++;

                if (count % 7 == 0)
                {
                    log.Debug("Dummy-Logging #{Counter}", count);
                }
                else if (count % 6 == 0)
                {
                    log.Warning("Dummy-Logging #{Counter}", count);
                }
                else if (count % 5 == 0)
                {
                    log.Error("Dummy-Logging #{Counter}", count);
                }
                else if (count % 4 == 0)
                {
                    log.Fatal("Dummy-Logging #{Counter}", count);
                }
                else
                {
                    log.Information("Dummy-Logging #{Counter}", count);
                }

                await Task.Delay(1000);
            }
        }
    }
}

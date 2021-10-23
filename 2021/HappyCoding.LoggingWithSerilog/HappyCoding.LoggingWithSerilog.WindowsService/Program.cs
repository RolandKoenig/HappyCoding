using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Topshelf;
using Topshelf.Configurators;

namespace HappyCoding.LoggingWithSerilog.WindowsService
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var rc = HostFactory.Run(x => 
            {
                x.Service<ServiceMain>(s =>  
                {
                    s.ConstructUsing(name=> new ServiceMain()); 
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("HappyCoding.LoggingWithSerilog.WindowsService");
                x.SetDisplayName("HappyCoding LoggingWithSerilog");
                x.SetServiceName("HappyCoding.LoggingWithSerilog");
            });

            var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }

        //*********************************************************************
        //*********************************************************************
        //*********************************************************************
        private sealed class ServiceMain
        {
            private Task? _appTask;
            private readonly CancellationTokenSource _appTaskCancelTokenSource;

            public ServiceMain()
            {
                _appTaskCancelTokenSource = new CancellationTokenSource();
            }

            public void Start()
            {
                _appTask = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .UseSerilog((context, services, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext())
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddHostedService<HostedService>();
                    })
                    .RunConsoleAsync(_appTaskCancelTokenSource.Token);
            }

            public void Stop()
            {
                _appTaskCancelTokenSource.Cancel();

                var appTask = _appTask;
                if (appTask != null)
                {
                    appTask.Wait();
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole.Themes;

namespace HappyCoding.LoggingWithSerilog.Console
{
    internal sealed class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ConsoleHostedService>();
                })
                .RunConsoleAsync();
        }
    }
}

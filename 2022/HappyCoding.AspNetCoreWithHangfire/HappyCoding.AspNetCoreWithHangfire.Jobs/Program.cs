using Microsoft.Extensions.Hosting;
using System.Reflection;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.AspNetCoreWithHangfire.Jobs;

public static class Program
{
    public static  Task Main(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .UseEnvironment("Development")
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(hostContext.Configuration.GetConnectionString("HangfireDB")));

                services.AddHangfireServer();

                services.AddHostedService<BackgroundJobBootstrap>();
            })
            .RunConsoleAsync();
    }
}

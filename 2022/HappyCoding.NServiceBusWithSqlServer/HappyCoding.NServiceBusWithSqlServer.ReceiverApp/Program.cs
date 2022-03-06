using System.Threading.Tasks;
using HappyCoding.NServiceBusWithSqlServer.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace HappyCoding.NServiceBusWithSqlServer.ReceiverApp
{
    internal class Program
    {
        static Task Main(string[] args)
        {
            return CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .EnsureNServiceBusQueuesCreated(Parameters.CONNECTION_STRING, Parameters.ENDPOINT_RECEIVER)
                .UseNServiceBus(_ =>
                {
                    var endpointConfiguration = new EndpointConfiguration(Parameters.ENDPOINT_RECEIVER);

                    var transport = endpointConfiguration
                        .UseTransport<SqlServerTransport>()
                        .ConnectionString(Parameters.CONNECTION_STRING);
                    
                    return endpointConfiguration;
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<HostedService>();
                });
    }
}

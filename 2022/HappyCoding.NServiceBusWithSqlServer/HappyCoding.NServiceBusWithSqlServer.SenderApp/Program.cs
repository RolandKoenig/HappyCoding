using System.Threading.Tasks;
using HappyCoding.NServiceBusWithSqlServer.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace HappyCoding.NServiceBusWithSqlServer.SenderApp
{
    internal class Program
    {
        static Task Main(string[] args)
        {
            return CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .EnsureNServiceBusQueuesCreated(Parameters.CONNECTION_STRING, Parameters.ENDPOINT_SENDER)
                .UseNServiceBus(_ =>
                {
                    var endpointConfiguration = new EndpointConfiguration(Parameters.ENDPOINT_SENDER);
                    endpointConfiguration.SendOnly();

                    var transport = endpointConfiguration
                        .UseTransport<SqlServerTransport>()
                        .ConnectionString(Parameters.CONNECTION_STRING);

                    transport.Routing()
                        .RouteToEndpoint(typeof(TestMessage), Parameters.ENDPOINT_RECEIVER);

                    return endpointConfiguration;
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<HostedService>();
                });
    }
}

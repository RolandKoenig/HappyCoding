using System;
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
                    
                    endpointConfiguration.Pipeline.Register(
                        typeof(ClearLargeExceptionsBehavior),
                        "Clear large exceptions");
                    
                    var transport = endpointConfiguration
                        .UseTransport<SqlServerTransport>()
                        .ConnectionString(Parameters.CONNECTION_STRING);
                    transport.Transport.TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive;

                    endpointConfiguration
                        .Recoverability()
                        .Immediate(x => x
                            .NumberOfRetries(1))
                        .Delayed(x => x
                            .NumberOfRetries(20)
                            .TimeIncrease(TimeSpan.FromSeconds(1)));

                    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
                    
                    return endpointConfiguration;
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<HostedService>();
                });
    }
}

using Avalonia;
using System;
using Grpc.Core;
using Grpc.Net.Client.Balancer;
using Grpc.Net.Client.Configuration;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using HappyCoding.GrpcCommunicationFeatures.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.FluentThemeDetection;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient;
internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseFluentThemeDetection()
            .UseDependencyInjection(services =>
            {
                // Common services
                services.AddLogging(loggingBuilder =>
                {
#if DEBUG
                    loggingBuilder.AddDebug();
#endif
                    loggingBuilder.AddConsole();
                });
                
                // ViewModels
                services.AddTransient<GrpcCommunicationViewModel>();

                // Shared services from this sample application
                services.AddSharedServices();

                // Add gRPC clients with load balancing
                services.AddGrpcStaticLoadBalancingForScheme(
                    "happycoding-srv",
                    new BalancerAddress("localhost", 5000),
                    new BalancerAddress("localhost", 5001),
                    new BalancerAddress("localhost", 5002),
                    new BalancerAddress("localhost", 5003));
                services.AddGrpcClient<Greeter.GreeterClient>(
                    options =>
                    {
                        options.Address = new Uri("happycoding-srv://myservice");
                        options.ChannelOptionsActions.Add(channelConfig =>
                        {
                            channelConfig.Credentials = ChannelCredentials.Insecure;
                            channelConfig.ServiceConfig = new ServiceConfig()
                            {
                                LoadBalancingConfigs =
                                {
                                    new RoundRobinConfig()
                                }
                            };
                        });
                        options.ConfigureSocketHttpHandler(socketHandler =>
                        {
                            socketHandler.KeepAlivePingDelay = TimeSpan.FromSeconds(30);
                            socketHandler.KeepAlivePingTimeout = TimeSpan.FromSeconds(5);
                            socketHandler.PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1);
                        });
                    })
                    .AddLoggingForOutgoingHttpCalls();
            })
            .LogToTrace();
}

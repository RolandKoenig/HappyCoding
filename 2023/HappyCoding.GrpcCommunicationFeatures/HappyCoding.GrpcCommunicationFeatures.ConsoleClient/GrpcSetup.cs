using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client;
using HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;
using Microsoft.Extensions.DependencyInjection;
using HappyCoding.GrpcCommunicationFeatures.Shared;

namespace HappyCoding.GrpcCommunicationFeatures.ConsoleClient;

internal static class GrpcSetup
{
    public static GrpcChannel SetupGrpcChannel()
    {
        return GrpcChannel.ForAddress($"http://localhost:5000");
    }

    public static GrpcChannel SetupGrpcChannelWithSocketHttpHandlerConfig()
    {
        var socketsHttpHandler = new SocketsHttpHandler();
        socketsHttpHandler.PooledConnectionIdleTimeout = TimeSpan.FromSeconds(5);

        var grpcChannelOptions = new GrpcChannelOptions()
        {
            HttpHandler = socketsHttpHandler,
        };
        
        return GrpcChannel.ForAddress($"http://localhost:5000", grpcChannelOptions);
    }

    public static GrpcChannel SetupGrpcChannelWithLoadBalancing()
    {
        var services = new ServiceCollection();
        services.AddGrpcStaticLoadBalancingForScheme(
            "happycoding-srv",
            new LoadBalancingTargetHost("localhost", 5000),
            new LoadBalancingTargetHost("localhost", 5001),
            new LoadBalancingTargetHost("localhost", 5002),
            new LoadBalancingTargetHost("localhost", 5003));

        return GrpcChannel.ForAddress(
            "happycoding-srv://myservice",
            new GrpcChannelOptions()
            {
                Credentials = ChannelCredentials.Insecure,
                ServiceProvider = services.BuildServiceProvider(),
                ServiceConfig = new ServiceConfig()
                {
                    LoadBalancingConfigs =
                    {
                        new RoundRobinConfig()
                    }
                }
            });
    }
}

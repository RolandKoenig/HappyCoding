using Grpc.Core;
using Grpc.Net.Client.Configuration;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using HappyCoding.GrpcCommunicationFeatures.Shared;
using HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

namespace HappyCoding.GrpcCommunicationFeatures.AspNetClient;

internal static class GrpcSetup
{
    public static void SetupGrpc(IServiceCollection services, IConfiguration configuration)
    {
        var serverConfig = configuration.GetSection("HappyCodingServer");
        var targetHosts = serverConfig
            .GetSection("Endpoints")
            .Get<LoadBalancingTargetHost[]>() ?? Array.Empty<LoadBalancingTargetHost>();
        var firstTargetHost = targetHosts.First();

        services.AddGrpcClient<Greeter.GreeterClient>(
            options =>
            {
                options.Address = new Uri($"http://{firstTargetHost.Host}:{firstTargetHost.Port}");
            })
            .AddLoggingForOutgoingHttpCalls();
    }

    public static void SetupGrpcWithSocketHttpHandlerConfig(IServiceCollection services, IConfiguration configuration)
    {
        var serverConfig = configuration.GetSection("HappyCodingServer");
        var targetHosts = serverConfig
            .GetSection("Endpoints")
            .Get<LoadBalancingTargetHost[]>() ?? Array.Empty<LoadBalancingTargetHost>();
        var firstTargetHost = targetHosts.First();

        services.AddGrpcClient<Greeter.GreeterClient>(
                options =>
                {
                    options.Address = new Uri($"http://{firstTargetHost.Host}:{firstTargetHost.Port}");
                    options.ConfigureSocketHttpHandler(socketHandler =>
                    {
                        socketHandler.KeepAlivePingDelay = TimeSpan.FromSeconds(30);
                        socketHandler.KeepAlivePingTimeout = TimeSpan.FromSeconds(5);
                        socketHandler.PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1);
                    });
                })
            .AddLoggingForOutgoingHttpCalls();
    }

    public static void SetupGrpcWithLoadBalancing(IServiceCollection services, IConfiguration configuration)
    {
        var serverConfig = configuration.GetSection("HappyCodingServer");
        var targetHosts = serverConfig
            .GetSection("Endpoints")
            .Get<LoadBalancingTargetHost[]>() ?? Array.Empty<LoadBalancingTargetHost>();

        services.AddGrpcStaticLoadBalancingForScheme(
            "happycoding-srv",
            targetHosts);
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
            })
            .AddLoggingForOutgoingHttpCalls();
    }
}

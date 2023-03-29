using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;
using Microsoft.Extensions.DependencyInjection;
using System;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using HappyCoding.GrpcCommunicationFeatures.Shared;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient;

internal static class GrpcSetup
{
    public static void SetupGrpc(IServiceCollection services)
    {
        services.AddGrpcClient<Greeter.GreeterClient>(
            options =>
            {
                options.Address = new Uri($"http://localhost:5000");
            })
            .AddLoggingForOutgoingHttpCalls();
    }

    public static void SetupGrpcWithSocketHttpHandlerConfig(IServiceCollection services)
    {
        services.AddGrpcClient<Greeter.GreeterClient>(
                options =>
                {
                    options.Address = new Uri($"http://localhost:5000");
                    options.ConfigureSocketHttpHandler(socketHandler =>
                    {
                        socketHandler.KeepAlivePingDelay = TimeSpan.FromSeconds(30);
                        socketHandler.KeepAlivePingTimeout = TimeSpan.FromSeconds(5);
                        socketHandler.PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1);
                    });
                })
            .AddLoggingForOutgoingHttpCalls();
    }

    public static void SetupGrpcWithLoadBalancing(IServiceCollection services)
    {
        services.AddGrpcStaticLoadBalancingForScheme(
            "happycoding-srv",
            new LoadBalancingTargetHost("localhost", 5000),
            new LoadBalancingTargetHost("localhost", 5001),
            new LoadBalancingTargetHost("localhost", 5002));
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

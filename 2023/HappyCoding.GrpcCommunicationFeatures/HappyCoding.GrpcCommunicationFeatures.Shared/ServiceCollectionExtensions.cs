using Grpc.Net.Client.Balancer;
using HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;
using HappyCoding.GrpcCommunicationFeatures.Shared.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.GrpcCommunicationFeatures.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
        => services.AddTransient<LoggingDelegatingHandler>();

    /// <summary>
    /// Adds a static list of hosts for the given url scheme.
    /// </summary>
    public static IServiceCollection AddGrpcStaticLoadBalancingForScheme(
        this IServiceCollection services,
        string scheme,
        params LoadBalancingTargetHost[] addresses)
        => services.AddSingleton<ResolverFactory, FixedHostsResolverFactory>(
            _ => new FixedHostsResolverFactory(scheme, addresses));
}

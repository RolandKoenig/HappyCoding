using HappyCoding.GrpcCommunicationFeatures.Shared.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.GrpcCommunicationFeatures.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
        => services.AddTransient<LoggingDelegatingHandler>();
}

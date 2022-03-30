using HappyCoding.CQSWithMediatR.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.CQSWithMediatR.InMemoryRepositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IPersonRepository, InMemoryPersonRepository>();
        return services;
    }
}
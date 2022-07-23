using HappyCoding.ConsoleLogWindow.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.InMemoryProcessGroupRepository;

public static class ServiceContainerExtensions
{
    public static IServiceCollection AddInEmoryProcessGroupRepository(this IServiceCollection services)
    {
        return services
            .AddSingleton<IProcessGroupRepository, InMemoryProcessGroupRepositoryImpl>();
    }
}

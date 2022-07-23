using HappyCoding.ConsoleLogWindow.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.StdOutProcessRunner;

public static class ServiceContainerExtensions
{
    public static IServiceCollection AddStdOutProcessRunner(this IServiceCollection services)
    {
        return services
            .AddSingleton<IProcessRunner, StdOutProcessRunnerImpl>();
    }
}
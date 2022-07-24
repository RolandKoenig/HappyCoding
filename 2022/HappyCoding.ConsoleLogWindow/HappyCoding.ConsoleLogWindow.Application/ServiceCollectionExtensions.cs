using HappyCoding.ConsoleLogWindow.Application.Services.UseCases;
using HappyCoding.ConsoleLogWindow.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUseCaseExecutor, UseCaseExecutor>();
    }

    public static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        return services
            .AddTransient<StartProcessUseCase>()
            .AddTransient<StopProcessUseCase>();
    }
}
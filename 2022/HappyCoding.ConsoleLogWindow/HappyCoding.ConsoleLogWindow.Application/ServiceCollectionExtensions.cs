using HappyCoding.ConsoleLogWindow.Application.Services.DocumentModelHandling;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;
using HappyCoding.ConsoleLogWindow.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUseCaseExecutor, UseCaseExecutor>()
            .AddSingleton<IDocumentModelProvider, DocumentModelProvider>();
    }

    public static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        return services
            .AddTransient<StartProcessUseCase>()
            .AddTransient<StopProcessUseCase>()
            .AddTransient<StopAllProcessesUseCase>()
            .AddTransient<LoadDocumentFromFileUseCase>()
            .AddTransient<SaveDocumentToFileUseCase>();
    }
}
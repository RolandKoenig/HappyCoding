using FluentValidation;
using HappyCoding.CQSWithMediatR.Application.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.CQSWithMediatR.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCQSWithMediatRApplication(this IServiceCollection services)
    {
        // For all the validators, register them with dependency injection as scoped
        var validators = AssemblyScanner.FindValidatorsInAssembly(typeof(ApplicationAssemblyMarker).Assembly, true);
        validators.ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
 
        // Add the custom pipeline validation to DI
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
using HappyCoding.HexagonalArchitecture.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.HexagonalArchitecture.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<CreateWorkshopCommandHandler>()
            .AddScoped<DeleteWorkshopCommandHandler>()
            .AddScoped<GetWorkshopQueryHandler>()
            .AddScoped<SearchWorkshopsQueryHandler>()
            .AddScoped<UpdateWorkshopCommandHandler>();
        
        return services;
    }
}
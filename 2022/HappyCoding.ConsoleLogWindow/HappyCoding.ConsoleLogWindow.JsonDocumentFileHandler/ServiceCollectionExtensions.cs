using HappyCoding.ConsoleLogWindow.Application.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.JsonDocumentFileHandler;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsonDocumentFileHandler(this IServiceCollection services)
        => services.AddSingleton<IDocumentFileHandler, JsonDocumentFileHandlerAdapter>();
}

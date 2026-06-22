using HappyCoding.AspNetCoreWindowsService.Middleware;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace HappyCoding.AspNetCoreWindowsService;

public static class Program
{
    public static void Main(string[] args)
    {
        // Set the current directory to the assembly location if running as a Windows Service
        if (WindowsServiceHelpers.IsWindowsService())
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
        }

        // Configure application
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseWindowsService(conf =>
        {
            conf.ServiceName = "HappyCoding.AspNetCoreWindowsService";
        });
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddWindowsService();
        
        // Configure the HTTP request pipeline.
        var app = builder.Build();
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.MapOpenApi();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}
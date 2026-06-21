using HappyCoding.AspNetCoreWindowsService.Middleware;

namespace HappyCoding.AspNetCoreWindowsService;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure application
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
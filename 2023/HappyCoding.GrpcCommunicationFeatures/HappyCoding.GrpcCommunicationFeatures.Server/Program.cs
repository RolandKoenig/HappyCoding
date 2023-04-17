using HappyCoding.GrpcCommunicationFeatures.GrpcServices;
using HappyCoding.GrpcCommunicationFeatures.Middlewares;
using HappyCoding.GrpcCommunicationFeatures.Shared;
using Microsoft.AspNetCore.HttpLogging;

namespace HappyCoding.GrpcCommunicationFeatures;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ###### Configure services
        
        // Shared services from this sample application
        builder.Services.AddSharedServices();

        // Add services to the container.
        builder.Services.AddGrpc();

        // ###### Configure request pipeline
        var app = builder.Build();
        app.UseMiddleware<RequestLoggingMiddleware>();
        
        app.MapGrpcService<GreeterService>();
        app.MapGrpcService<ServerSideStreamingService>();
        
        app.Run();
    }
}
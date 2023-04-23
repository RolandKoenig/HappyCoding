using HappyCoding.GrpcCommunicationFeatures.Server.GrpcServices;
using HappyCoding.GrpcCommunicationFeatures.Server.Middlewares;
using HappyCoding.GrpcCommunicationFeatures.Shared;

namespace HappyCoding.GrpcCommunicationFeatures.Server;

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

        // gRPC
        app.MapGrpcService<GreeterService>();
        app.MapGrpcService<ServerSideStreamingService>();
        app.MapGrpcService<BidirectionalStreamingService>();
        
        app.Run();
    }
}
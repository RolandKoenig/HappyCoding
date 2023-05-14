using HappyCoding.GrpcWithNet48.AspNetCoreServer.Services;

namespace HappyCoding.GrpcWithNet48.AspNetCoreServer;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseGrpcWeb(new GrpcWebOptions(){ DefaultEnabled = true });
        app.MapGrpcService<GreeterService>();
        app.MapGrpcService<ServerSideStreamingService>();

        app.Run();
    }
}
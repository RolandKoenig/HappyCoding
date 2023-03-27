using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using HappyCoding.GrpcCommunicationFeatures.Shared;

namespace HappyCoding.GrpcCommunicationFeatures.AspNetClient;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Shared services from this sample application
        builder.Services.AddSharedServices();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddGrpcClient<Greeter.GreeterClient>(
            options =>
            {
                options.Address = new Uri("http://localhost:5000");
                options.ConfigureSocketHttpHandler(socketHandler =>
                {
                    socketHandler.KeepAlivePingDelay = TimeSpan.FromSeconds(30);
                    socketHandler.KeepAlivePingTimeout = TimeSpan.FromSeconds(5);
                    socketHandler.PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1);
                });
            })
            .AddLoggingForOutgoingHttpCalls();

        // Build pipeline
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}

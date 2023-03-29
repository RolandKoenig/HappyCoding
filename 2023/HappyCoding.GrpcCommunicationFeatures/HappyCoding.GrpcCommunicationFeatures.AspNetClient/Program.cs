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

        // Add gRPC
        GrpcSetup.SetupGrpc(builder.Services, builder.Configuration);
        // GrpcSetup.SetupGrpcWithSocketHttpHandlerConfig(builder.Services, builder.Configuration);
        // GrpcSetup.SetupGrpcWithLoadBalancing(builder.Services, builder.Configuration);

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

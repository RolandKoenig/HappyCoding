using Grpc.Core;
using Grpc.Net.Client.Configuration;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using HappyCoding.GrpcCommunicationFeatures.Shared;
using HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

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

        // Add gRPC clients with load balancing
        builder.Services.AddGrpcStaticLoadBalancingForScheme(
            "happycoding-srv",
            builder.Configuration
                .GetSection("HappyCodingServer")?
                .GetSection("Endpoints")?
                .Get<LoadBalancingTargetHost[]>() ?? Array.Empty<LoadBalancingTargetHost>());
        builder.Services.AddGrpcClient<Greeter.GreeterClient>(
                options =>
                {
                    options.Address = new Uri("happycoding-srv://myservice");
                    options.ChannelOptionsActions.Add(channelConfig =>
                    {
                        channelConfig.Credentials = ChannelCredentials.Insecure;
                        channelConfig.ServiceConfig = new ServiceConfig()
                        {
                            LoadBalancingConfigs =
                            {
                                new RoundRobinConfig()
                            }
                        };
                    });
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

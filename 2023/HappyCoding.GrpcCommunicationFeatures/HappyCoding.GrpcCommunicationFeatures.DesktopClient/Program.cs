using Avalonia;
using System;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using HappyCoding.GrpcCommunicationFeatures.Shared;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.FluentThemeDetection;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient;
internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseFluentThemeDetection()
            .UseDependencyInjection(services =>
            {
                // ViewModels
                services.AddTransient<GrpcCommunicationViewModel>();

                // Shared services from this sample application
                services.AddSharedServices();

                // Add gRPC clients
                services.AddGrpcClient<Greeter.GreeterClient>(
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
            })
            .LogToTrace();
}

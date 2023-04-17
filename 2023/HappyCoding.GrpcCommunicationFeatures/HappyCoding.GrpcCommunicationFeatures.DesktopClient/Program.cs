using Avalonia;
using System;
using HappyCoding.GrpcCommunicationFeatures.DesktopClient.Views;
using HappyCoding.GrpcCommunicationFeatures.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                // Common services
                services.AddLogging(loggingBuilder =>
                {
#if DEBUG
                    loggingBuilder.AddDebug();
#endif
                    loggingBuilder.AddConsole();
                });
                
                // ViewModels
                services.AddTransient<GrpcGreeterClientViewModel>();
                services.AddTransient<GrpcServerSideStreamingViewModel>();
                services.AddTransient<GrpcBidirectionalStreamingViewModel>();

                // Shared services from this sample application
                services.AddSharedServices();

                // Add gRPC
                // GrpcSetup.SetupGrpc(services);
                // GrpcSetup.SetupGrpcWithSocketHttpHandlerConfig(services);
                GrpcSetup.SetupGrpcWithLoadBalancing(services);
            })
            .LogToTrace();
}

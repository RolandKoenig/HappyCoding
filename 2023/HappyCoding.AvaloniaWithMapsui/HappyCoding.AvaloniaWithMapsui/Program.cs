using Avalonia;
using System;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.FluentThemeDetection;

namespace HappyCoding.AvaloniaWithMapsui;

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
            .LogToTrace()
            .UsePlatformDetect()
            .UseFluentThemeDetection()
            .UseDependencyInjection(services =>
            {
                // Base services
                var messenger = new StrongReferenceMessenger();
                services.AddSingleton<IMessenger>(messenger);
                
                // Modules
                FilesModule.Bootstrap.Load(services, messenger);
                MapsModule.Bootstrap.Load(services, messenger);
                SelectionModule.Bootstrap.Load(services, messenger);
            });
}

using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection;
using Testing.MinioClientApp.MinioHandling;
using Testing.MinioClientApp.Views;

namespace Testing.MinioClientApp
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseDependencyInjection(services =>
                {
                    // Singletons
                    services.AddSingleton<MinioInterfaceConfiguration>();
                    services.AddSingleton<MinioInterface>();
                    
                    // ViewModels
                    services.AddTransient<SettingsViewModel>();
                    services.AddTransient<UploadViewModel>();
                })
                .LogToTrace();
    }
}
using System;
using Avalonia;
using HappyCoding.CommunityToolkitMvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.CommunityToolkitMvvm
{
    class Program
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
                .LogToTrace()
                .UseDependencyInjection(services =>
                {
                    // ViewModels
                    services.AddTransient<MainWindowViewModel>();
                });
    }
}
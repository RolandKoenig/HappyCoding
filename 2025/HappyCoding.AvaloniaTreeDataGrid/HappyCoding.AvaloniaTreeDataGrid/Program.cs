using Avalonia;
using System;
using HappyCoding.AvaloniaTreeDataGrid.Services;
using HappyCoding.AvaloniaTreeDataGrid.Views;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection;

namespace HappyCoding.AvaloniaTreeDataGrid;

public static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    // ReSharper disable once MemberCanBePrivate.Global
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .UseDependencyInjection(services =>
            {
                // Services
                services.AddSingleton<ITestDataGenerator, BogusTestDataGenerator>();
                
                // ViewModels
                services
                    .AddTransient<MainWindowViewModel>()
                    .AddTransient<FlatDataViewModel>()
                    .AddTransient<HierarchicalDataViewModel>();
            })
            .LogToTrace();
}
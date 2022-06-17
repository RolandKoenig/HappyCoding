using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.Gui;

public partial class App : Application
{
    public IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        this.Services = this.ConfigureServices();

        AvaloniaXamlLoader.Load(this);
    }

    // Setup dependency injection
    private IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        // Register services

        // Register view models
        serviceCollection.AddTransient<MainWindowViewModel>();

        return serviceCollection.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HappyCoding.ConsoleLogWindow.Application;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Gui.Model;
using HappyCoding.ConsoleLogWindow.Gui.Views;
using HappyCoding.ConsoleLogWindow.Messenger;
using HappyCoding.ConsoleLogWindow.StdOutProcessRunner;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.Gui;

public partial class App : Avalonia.Application
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

        // Register services for infrastructure
        serviceCollection.AddFirLibMessenger();

        // Register services from adapters
        serviceCollection.AddStdOutProcessRunner();

        // Register view models
        serviceCollection.AddTransient<MainWindowViewModel>();
        serviceCollection.AddTransient<ProcessGroupsViewModel>();
        serviceCollection.AddTransient<RunningProcessViewModel>();

        // Register document model provider
        serviceCollection.AddSingleton<IDocumentModelProvider, DesktopDocumentModelProvider>();

        // Register application 
        serviceCollection.AddApplicationServices();
        serviceCollection.AddApplicationUseCases();

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
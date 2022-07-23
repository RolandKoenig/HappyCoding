using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HappyCoding.ConsoleLogWindow.Gui.Views;
using HappyCoding.ConsoleLogWindow.InMemoryProcessGroupRepository;
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

        // Register services
        serviceCollection.AddInEmoryProcessGroupRepository();
        serviceCollection.AddStdOutProcessRunner();

        // Register view models
        serviceCollection.AddTransient<MainWindowViewModel>();
        serviceCollection.AddTransient<ProcessGroupsViewModel>();

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
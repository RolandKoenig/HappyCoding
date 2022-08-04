using System;
using HappyCoding.ConsoleLogWindow.Application;
using HappyCoding.ConsoleLogWindow.Gui.Views;
using HappyCoding.ConsoleLogWindow.Gui.Views.ProcessGroups;
using HappyCoding.ConsoleLogWindow.JsonDocumentFileHandler;
using HappyCoding.ConsoleLogWindow.Messenger;
using HappyCoding.ConsoleLogWindow.StdOutProcessRunner;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.Gui;

internal static class Startup
{
    /// <summary>
    /// Configure services for dependency injection
    /// </summary>
    public static IServiceProvider ConfigureServices(StartupArguments args)
    {
        var serviceCollection = new ServiceCollection();

        // Startup arguments
        serviceCollection.AddSingleton(args);

        // Register services for infrastructure
        serviceCollection.AddFirLibMessenger();

        // Register services from adapters
        serviceCollection.AddStdOutProcessRunner();
        serviceCollection.AddJsonDocumentFileHandler();

        // Register view models
        serviceCollection.AddTransient<MainWindowViewModel>();
        serviceCollection.AddTransient<ProcessGroupsViewModel>();
        serviceCollection.AddTransient<RunningProcessViewModel>();
        serviceCollection.AddTransient<ProcessStatusViewModel>();

        // Register application 
        serviceCollection.AddApplicationServices();
        serviceCollection.AddApplicationUseCases();

        return serviceCollection.BuildServiceProvider();
    }
}

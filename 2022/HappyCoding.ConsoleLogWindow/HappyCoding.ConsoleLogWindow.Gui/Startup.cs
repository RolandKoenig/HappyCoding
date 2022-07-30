using System;
using HappyCoding.ConsoleLogWindow.Application;
using HappyCoding.ConsoleLogWindow.Gui.Views;
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
    public static IServiceProvider ConfigureServices(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        // Register services for infrastructure
        serviceCollection.AddFirLibMessenger();

        // Register services from adapters
        serviceCollection.AddStdOutProcessRunner();
        serviceCollection.AddJsonDocumentFileHandler();

        // Register view models
        serviceCollection.AddTransient<MainWindowViewModel>();
        serviceCollection.AddTransient<ProcessGroupsViewModel>();
        serviceCollection.AddTransient<RunningProcessViewModel>();

        // Register application 
        serviceCollection.AddApplicationServices();
        serviceCollection.AddApplicationUseCases();

        return serviceCollection.BuildServiceProvider();
    }
}

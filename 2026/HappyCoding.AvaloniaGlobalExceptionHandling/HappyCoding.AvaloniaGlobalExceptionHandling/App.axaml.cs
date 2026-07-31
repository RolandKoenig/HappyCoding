using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace HappyCoding.AvaloniaGlobalExceptionHandling;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        Dispatcher.UnhandledException += OnDispatcher_UnhandledException;
        TaskScheduler.UnobservedTaskException += OnTaskScheduler_UnobservedTaskException;
        AppDomain.CurrentDomain.UnhandledException += OnCurrentDomain_UnhandledException;
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void OnDispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Console.WriteLine($"Exception {e.Exception.GetType().FullName} catched, Message: {e.Exception.Message}");
        e.Handled = true;
    }
    
    private void OnTaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        Console.WriteLine($"Exception {e.Exception.GetType().FullName} recognized, Message: {e.Exception.Message}");
    }
    
    private void OnCurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Console.WriteLine($"Exception {e.ExceptionObject.GetType().FullName} recognized");
    }
}
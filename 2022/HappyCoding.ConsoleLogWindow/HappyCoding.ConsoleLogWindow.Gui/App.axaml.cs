using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HappyCoding.ConsoleLogWindow.Gui.Views;

namespace HappyCoding.ConsoleLogWindow.Gui;

public partial class App : Avalonia.Application
{
    public IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        var classicDesktopApplicationLifetime = this.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        var args = classicDesktopApplicationLifetime?.Args ?? Array.Empty<string>();

        this.Services = Startup.ConfigureServices(args);

        AvaloniaXamlLoader.Load(this);
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
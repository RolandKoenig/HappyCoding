using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HappyCoding.GRpcCommunication.ServerApp.Views;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp;
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var viewMessenger = new FirLibMessenger();
        viewMessenger.ConnectToGlobalMessaging(
            FirLibMessengerThreadingBehavior.EnsureMainSyncContextOnSyncCalls,
            ViewConstants.VIEW_MESSENGER_NAME,
            SynchronizationContext.Current);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}

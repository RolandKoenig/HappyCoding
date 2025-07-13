using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable once LoopVariableIsNeverChangedInsideLoop

namespace HappyCoding.TemperatureViewer.Embedded;

internal static class EmbeddedConsoleUtil
{
    internal static IDisposable SilenceConsole()
    {
        Console.CancelKeyPress += OnConsole_CancelKeyPress;
        
        var isRunning = true;
        Console.CursorVisible = false;
        
        var consoleListenerThread = new Thread(() =>
        {
            while (isRunning)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    if (Application.Current?.ApplicationLifetime is IControlledApplicationLifetime controlledApplicationLifetime)
                    {
                        controlledApplicationLifetime.Shutdown(0);
                    }
                }
            }
        });
        consoleListenerThread.IsBackground = true;
        consoleListenerThread.Start();

        return new DummyDisposable(
            () =>
            {
                isRunning = false;
                Console.CursorVisible = true;
            });
    }

    private static void OnConsole_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IControlledApplicationLifetime controlledApplicationLifetime)
        {
            e.Cancel = true;
            controlledApplicationLifetime.Shutdown(0);
        }
    }
}
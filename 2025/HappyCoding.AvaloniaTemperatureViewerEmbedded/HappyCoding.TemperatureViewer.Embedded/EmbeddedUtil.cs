using System;
using System.Threading;

// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable once LoopVariableIsNeverChangedInsideLoop

namespace HappyCoding.TemperatureViewer.Embedded;

internal static class EmbeddedUtil
{
    internal static IDisposable SilenceConsole()
    {
        var isRunning = true;
        Console.CursorVisible = false;
        
        var consoleListenerThread = new Thread(() =>
        {
            while (isRunning)
            {
                Console.ReadKey(true);
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
}
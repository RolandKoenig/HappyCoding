using System;
using System.Threading;

namespace HappyCoding.TemperatureViewer.Embedded;

public static class EmbeddedUtil
{
    internal static void SilenceConsole()
    {
        new Thread(() =>
            {
                Console.CursorVisible = false;
                while (true)
                {
                    Console.ReadKey(true);
                }
            })
            { IsBackground = true }.Start();
    }
}
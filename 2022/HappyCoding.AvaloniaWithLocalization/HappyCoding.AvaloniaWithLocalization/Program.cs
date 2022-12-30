using Avalonia;
using System;
using System.Globalization;
using System.Threading;

// ReSharper disable LocalizableElement

namespace HappyCoding.AvaloniaWithLocalization;

internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Console.WriteLine("Choose language");
        Console.WriteLine(" 1 = no change");
        Console.WriteLine(" 2 = de-DE");
        Console.WriteLine(" 3 = en-US");
        Console.WriteLine();

        Console.Write("Language: ");
        var languageID = Console.ReadLine();

        switch (languageID)
        {
            case "1":
                break;

            case "2":
                Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
                break;

            case "3":
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                break;

            default:
                Console.WriteLine("Input not recognized, choosing option 1");
                goto case "1";
        }

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    /// <summary>
    /// Avalonia configuration
    /// </summary>
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}

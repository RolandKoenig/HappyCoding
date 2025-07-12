using Avalonia;
using System;

namespace HappyCoding.TemperatureViewer.Embedded;

public static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        using var consoleListener = EmbeddedUtil.SilenceConsole();

        BuildAvaloniaApp()
            .StartLinuxDrm(args: args, card: null, scaling: 1.0);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    // ReSharper disable once MemberCanBePrivate.Global
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
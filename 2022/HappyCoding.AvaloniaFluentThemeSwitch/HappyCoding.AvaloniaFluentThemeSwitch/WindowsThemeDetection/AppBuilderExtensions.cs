using System;
using System.Runtime.InteropServices;
using System.Threading;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Themes.Fluent;

namespace HappyCoding.AvaloniaFluentThemeSwitch.WindowsThemeDetection;

public static class AppBuilderExtensions
{
    /// <summary>
    /// Uses windows theme detector to set the currently active FluentTheme
    /// </summary>
    public static AppBuilder UseWindowsThemeDetectorOnWindowsPlatform(this AppBuilder appBuilder, Action<FluentThemeMode> setThemeAction)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return appBuilder; }

        appBuilder.AfterSetup(_ =>
        {
            // This call prevents warnings from the compiler
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return; }

            setThemeAction(WindowsThemeDetector.GetFluentThemeByCurrentWindowsTheme());

            var syncContext = SynchronizationContext.Current;
            if (syncContext == null)
            {
                Logger.Sink?.Log(
                    LogEventLevel.Error,
                    typeof(AppBuilderExtensions).Namespace ?? "",
                    null,
                    "Unable to get SynchronizationContext from UI thread. Automated theme switch will not work");
                return;
            }

            WindowsThemeDetector.ListenForThemeChangeEvent(fluentThemeMode =>
            {
                syncContext.Post(
                    _ => setThemeAction(fluentThemeMode),
                    null);
            });
        });

        return appBuilder;
    }
}

using System;
using System.Linq;
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
    public static AppBuilder UseWindowsThemeDetectorOnWindowsPlatform(
        this AppBuilder appBuilder, 
        Action<FluentThemeMode>? setThemeAction = null)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return appBuilder; }

        appBuilder.AfterSetup(_ =>
        {
            // This call prevents warnings from the compiler
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return; }

            var initialThemeMode = WindowsThemeDetector.GetFluentThemeByCurrentWindowsTheme();
            if (setThemeAction != null) { setThemeAction(initialThemeMode); }
            else { TrySetThemeOnCurrentApplication(initialThemeMode); }

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
                    _ =>
                    {
                        if (setThemeAction != null) { setThemeAction(fluentThemeMode); }
                        else { TrySetThemeOnCurrentApplication(fluentThemeMode); }
                    },
                    null);
            });
        });

        return appBuilder;
    }

    private static void TrySetThemeOnCurrentApplication(FluentThemeMode themeMode)
    {
        var currentApplication = Application.Current;
        if (currentApplication == null) { return; }

        var fluentTheme = currentApplication.Styles.FirstOrDefault(x => x.GetType() == typeof(FluentTheme));
        if (fluentTheme == null)
        {
            Logger.Sink?.Log(
                LogEventLevel.Error,
                typeof(AppBuilderExtensions).Namespace ?? "",
                null,
                "Unable to find FluentTheme object on current Application. Automated theme switch does not work");
            return;
        }

        ((FluentTheme) fluentTheme).Mode = themeMode;
    }
}

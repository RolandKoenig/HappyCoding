using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;

namespace HappyCoding.AvaloniaFluentThemeSwitch;

public partial class App : Application
{
    private static readonly FluentTheme s_fluentTheme = new(new Uri("avares://ControlCatalog/Styles"));

    /// <summary>
    /// Manually sets current theme.
    /// </summary>
    public static void SetFluentThemeMode(FluentThemeMode mode)
    {
        s_fluentTheme.Mode = mode;
    }

    public override void Initialize()
    {
        this.Styles.Insert(0, s_fluentTheme);

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}

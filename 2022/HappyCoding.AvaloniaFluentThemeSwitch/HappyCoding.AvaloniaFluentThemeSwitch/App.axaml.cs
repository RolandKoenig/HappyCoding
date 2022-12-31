using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;

namespace HappyCoding.AvaloniaFluentThemeSwitch;

public partial class App : Application
{
    private static readonly FluentTheme s_fluentTheme = new(new Uri("avares://ControlCatalog/Styles"));
    private static readonly StyleInclude s_dataGridFluentStyle = new(new Uri("avares://ControlCatalog/Styles"))
    {
        Source = new Uri("avares://Avalonia.Controls.DataGrid/Themes/Fluent.xaml")
    };

    public static void SetFluentThemeMode(FluentThemeMode mode)
    {
        s_fluentTheme.Mode = mode;
    }

    public override void Initialize()
    {
        this.Styles.Insert(0, s_fluentTheme);
        this.Styles.Insert(1, s_dataGridFluentStyle);

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

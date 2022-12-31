using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Themes.Fluent;

namespace HappyCoding.AvaloniaFluentThemeSwitch;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private void OnThemeLightClick(object? sender, RoutedEventArgs e)
    {
        App.SetFluentThemeMode(FluentThemeMode.Light);
    }

    private void OnThemeDarkClick(object? sender, RoutedEventArgs e)
    {
        App.SetFluentThemeMode(FluentThemeMode.Dark);
    }
}

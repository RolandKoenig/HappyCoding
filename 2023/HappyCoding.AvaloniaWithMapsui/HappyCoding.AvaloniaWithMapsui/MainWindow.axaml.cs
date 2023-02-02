using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Themes.Fluent;
using RolandK.AvaloniaExtensions.FluentThemeDetection;
using RolandK.AvaloniaExtensions.Mvvm.Markup;

namespace HappyCoding.AvaloniaWithMapsui;

public partial class MainWindow : MvvmWindow
{
    public MainWindow()
    {
        this.InitializeComponent();
    }
    
    private void OnMnuSetThemeLight_Click(object? sender, RoutedEventArgs e)
    {
        Application.Current.TrySetFluentThemeMode(FluentThemeMode.Light);
    }
    
    private void OnMnuSetThemeDark_Click(object? sender, RoutedEventArgs e)
    {
        Application.Current.TrySetFluentThemeMode(FluentThemeMode.Dark);
    }
}

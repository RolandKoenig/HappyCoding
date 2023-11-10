using System;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace HappyCoding.WpfHost;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ResetWebView()
    {
        this.CtrlWebView.Source = new Uri("https://localhost:5002");
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        this.ResetWebView();
        this.CtrlWebView.CoreWebView2InitializationCompleted += OnCtrlWebView2_CoreWebView2InitializationCompleted;
    }

    private void OnCtrlWebView2_CoreWebView2InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        
    }

    private void OnMnuReset_Click(object sender, RoutedEventArgs e)
    {
        this.ResetWebView();
    }
}

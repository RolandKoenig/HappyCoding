using System.Windows;

namespace HappyCoding.WpfWithMoreUiThreads;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnMnuOpenWindowInNewUiThread_Click(object sender, RoutedEventArgs e)
    {
        WindowWithCustomThread.ShowInNewUihread();
    }
}
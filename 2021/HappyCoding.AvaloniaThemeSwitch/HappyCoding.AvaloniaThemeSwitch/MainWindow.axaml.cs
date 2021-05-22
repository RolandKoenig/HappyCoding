using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using GalaSoft.MvvmLight.Messaging;

namespace HappyCoding.AvaloniaThemeSwitch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnMnuThemeLight_Click(object? sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(new ThemeChangeRequestMessage(FluentThemeMode.Light));
        }

        private void OnMnuThemeDark_Click(object? sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(new ThemeChangeRequestMessage(FluentThemeMode.Dark));
        }
    }
}

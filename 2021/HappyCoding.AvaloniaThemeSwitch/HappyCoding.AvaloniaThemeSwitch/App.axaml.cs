using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
using GalaSoft.MvvmLight.Messaging;

namespace HappyCoding.AvaloniaThemeSwitch
{
    public class App : Application
    {
        private FluentTheme _resThemeLight;
        private FluentTheme _resThemeDark;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            _resThemeLight = (FluentTheme)this.Resources["ResThemeLight"]!;
            _resThemeDark = (FluentTheme) this.Resources["ResThemeDark"]!;
            this.Styles.Insert(0, _resThemeLight);

            Messenger.Default.Register<ThemeChangeRequestMessage>(this, this.OnMessageReceived_ThemeChangeRequest); 

            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnMessageReceived_ThemeChangeRequest(ThemeChangeRequestMessage message)
        {
            switch (message.Mode)
            {
                case FluentThemeMode.Light:
                    this.Styles[0] = _resThemeLight;
                    break;

                case FluentThemeMode.Dark:
                    this.Styles[0] = _resThemeDark;
                    break;
            }
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HappyCoding.SimpleWinUI3App.Pages;

public sealed partial class Home : Page
{
    public Home()
    {
        this.InitializeComponent();
    }

    public void SetDarkTheme()
    {
        var rootElement = (FrameworkElement)this.XamlRoot.Content;
        rootElement.RequestedTheme = ElementTheme.Dark;
    }

    public void SetLightTheme()
    {
        var rootElement = (FrameworkElement)this.XamlRoot.Content;
        rootElement.RequestedTheme = ElementTheme.Light;
    }
}

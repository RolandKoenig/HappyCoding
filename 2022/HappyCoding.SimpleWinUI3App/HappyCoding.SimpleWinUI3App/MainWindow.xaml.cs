using System;
using System.Linq;
using Windows.ApplicationModel;
using HappyCoding.SimpleWinUI3App.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace HappyCoding.SimpleWinUI3App;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();

        this.Title = "Simple WinUI 3 App";
        this.ExtendsContentIntoTitleBar = true;  // enable custom titlebar
        this.SetTitleBar(CtrlAppTitleBar);      // set user ui element as titlebar

        if (!DesignMode.DesignModeEnabled)
        {
            CtrlNavView.SelectedItem = CtrlNavView.MenuItems.FirstOrDefault();
            CtrlMainFrame.Navigate(typeof(Home));
        }
    }

    public async void ShowUnhandledException(Exception ex)
    { 
        var dialog = new ContentDialog();
        
        dialog.XamlRoot = CtrlNavView.XamlRoot;
        dialog.Title = "Unhandled Exception";
        dialog.PrimaryButtonText = "OK";
        dialog.DefaultButton = ContentDialogButton.Primary;
        dialog.Content = ex.ToString();

        await dialog.ShowAsync();
    }

    public void NavigateTo(object sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer.Tag is not string targetTypeName)
        {
            return;
        }

        var targetType = Type.GetType(targetTypeName);
        if (targetType == null)
        {
            return;
        }

        CtrlNavView.Header = args.InvokedItemContainer.Content.ToString();
        CtrlMainFrame.Navigate(targetType);
    }

    public void NavigateBack()
    {
        CtrlMainFrame.GoBack();
    }

    private void CtrlMainFrame_OnNavigated(object sender, NavigationEventArgs e)
    {
        // Apply selection (also on navigating back)
        var newSelection = CtrlNavView.MenuItems.FirstOrDefault(
            x =>
            {
                if ((x is NavigationViewItem item) &&
                    (item.Tag as string == CtrlMainFrame.Content?.GetType().FullName))
                {
                    return true;
                }
                return false;
            });
        if (CtrlNavView.SelectedItem != newSelection)
        {
            CtrlNavView.SelectedItem = newSelection;
        }

        // Apply header
        if (CtrlNavView.SelectedItem != null)
        {
            CtrlNavView.Header = ((NavigationViewItem) CtrlNavView.SelectedItem).Content.ToString();
        }
        else
        {
            CtrlNavView.Header = "";
        }
    }
}
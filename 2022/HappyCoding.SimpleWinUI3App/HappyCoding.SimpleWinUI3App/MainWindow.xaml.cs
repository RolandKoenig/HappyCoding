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
            CtrlMainFrame.Navigate(typeof(Home));
        }
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

        CtrlMainFrame.Navigate(targetType);
    }

    public void NavigateBack()
    {
        CtrlMainFrame.GoBack();
    }

    private void CtrlMainFrame_OnNavigated(object sender, NavigationEventArgs e)
    {
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
    }
}
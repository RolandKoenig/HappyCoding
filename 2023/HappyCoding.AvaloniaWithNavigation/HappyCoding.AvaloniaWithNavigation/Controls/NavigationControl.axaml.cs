using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using RolandK.AvaloniaExtensions.Mvvm.Markup;

namespace HappyCoding.AvaloniaWithNavigation.Controls;

public partial class NavigationControl : ViewServiceHostUserControl
{
    public List<NavigationItem> NavigationItems { get; } = new List<NavigationItem>();

    public string InitialViewName { get; set; } = string.Empty;

    public NavigationControl()
    {
        this.ViewServices.Add(new NavigationViewService(this));

        this.InitializeComponent();
    }

    public void NavigateTo(string targetName)
    {
        if (!this.TryNavigateTo(targetName))
        {
            throw new ArgumentException($"Unable to navigate to '{targetName}'!");
        }
    }

    /// <summary>
    /// Navigates to the Control with the given name.
    /// </summary>
    public bool TryNavigateTo(string targetName)
    {
        var navigationTarget = this.NavigationItems.FirstOrDefault(
            x => x.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase));
        if ((navigationTarget == null) ||
            (navigationTarget.ViewType == null))
        {
            return false;
        }

        var viewObject = Activator.CreateInstance(navigationTarget.ViewType);
        if (viewObject is not Control viewObjectControl)
        {
            return false;
        }

        this.Content = viewObjectControl;
        return true;
    }

    private void TriggerInitialNavigation()
    {
        if (this.Content != null) { return; }
        if (string.IsNullOrEmpty(this.InitialViewName)) { return; }

        this.TryNavigateTo(this.InitialViewName);
    }

    /// <inheritdoc />
    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnAttachedToLogicalTree(e);

        // Trigger update of this control's state
        Dispatcher.UIThread.Post(this.TriggerInitialNavigation);
    }
}
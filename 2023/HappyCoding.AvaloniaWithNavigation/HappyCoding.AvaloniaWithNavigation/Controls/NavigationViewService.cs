using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace HappyCoding.AvaloniaWithNavigation.Controls;

internal class NavigationViewService : ViewServiceBase, INavigationViewService
{
    private readonly NavigationControl _navigationControl;

    public NavigationViewService(NavigationControl navigationControl)
    {
        _navigationControl = navigationControl;
    }

    /// <inheritdoc />
    public void NavigateTo(string targetName)
    {
        _navigationControl.NavigateTo(targetName);
    }

    /// <inheritdoc />
    public bool TryNavigateTo(string targetName)
    {
        return _navigationControl.TryNavigateTo(targetName);
    }
}

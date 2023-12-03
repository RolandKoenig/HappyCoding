namespace HappyCoding.AvaloniaWithNavigation.Controls;

public interface INavigationViewService
{
    void NavigateTo(string targetName);

    bool TryNavigateTo(string targetName);
}

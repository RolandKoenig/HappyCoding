using CommunityToolkit.Mvvm.Input;
using HappyCoding.AvaloniaWithNavigation.Controls;
using HappyCoding.AvaloniaWithNavigation.Util;

namespace HappyCoding.AvaloniaWithNavigation.Views;

internal partial class HomeViewModel : OwnViewModelBase
{
    [RelayCommand]
    private void NavigateToNext()
    {
        var srvNavigation = this.GetViewService<INavigationViewService>();
        srvNavigation.NavigateTo("ToDoList");
    }
}

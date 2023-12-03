using CommunityToolkit.Mvvm.ComponentModel;
using HappyCoding.AvaloniaWithNavigation.Util;
using HappyCoding.AvaloniaWithNavigation.Views;

namespace HappyCoding.AvaloniaWithNavigation;

internal partial class MainWindowViewModel : OwnViewModelBase
{
    [ObservableProperty] private object _currentViewModel;

    public MainWindowViewModel()
    {
        var homeViewModel = new HomeViewModel();
        this.CurrentViewModel = homeViewModel;
    }


}

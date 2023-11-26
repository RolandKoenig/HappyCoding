using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaWithAcrylicBackground;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [property: ReadOnly(true)]
    public ObservableCollection<string> _listItems = new ObservableCollection<string>();

    public MainWindowViewModel()
    {
        _listItems.Add("Item 1");
        _listItems.Add("Item 2");
        _listItems.Add("Item 3");
    }
}
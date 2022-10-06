using System.Collections.ObjectModel;
using HappyCoding.AvaloniaAppWithDataGrid.Data;
using HappyCoding.AvaloniaAppWithDataGrid.Util;

namespace HappyCoding.AvaloniaAppWithDataGrid.Views;

public class FilteredViewModel : ViewModelBase
{
    private TestDataRow? _selectedItem;
    
    public ObservableCollection<TestDataRow> Items { get; }
}
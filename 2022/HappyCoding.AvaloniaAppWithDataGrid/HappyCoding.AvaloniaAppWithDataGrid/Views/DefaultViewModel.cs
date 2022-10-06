using System.Collections.ObjectModel;
using System.Linq;
using HappyCoding.AvaloniaAppWithDataGrid.Data;
using HappyCoding.AvaloniaAppWithDataGrid.Util;

namespace HappyCoding.AvaloniaAppWithDataGrid.Views;

public class DefaultViewModel : ViewModelBase
{
    private TestDataRow? _selectedItem;
    
    public ObservableCollection<TestDataRow> Items { get; }

    public TestDataRow? SelectedItem
    {
        get => _selectedItem;
        set => SetField(ref _selectedItem, value);
    }

    public DefaultViewModel()
    {
        this.Items = new ObservableCollection<TestDataRow>(
            TestDataFactory.LoadTestData(100, 500));
        this.SelectedItem = this.Items.FirstOrDefault();
    }
}
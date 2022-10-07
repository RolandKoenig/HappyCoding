using System.Linq;
using Avalonia.Collections;
using HappyCoding.AvaloniaAppWithDataGrid.Data;
using HappyCoding.AvaloniaAppWithDataGrid.Util;

namespace HappyCoding.AvaloniaAppWithDataGrid.Views;

public class WithGroupAndFilterViewModel : ViewModelBase
{
    private TestDataRow? _selectedItem;

    private DataGridCollectionView Items { get; }

    public TestDataRow? SelectedItem
    {
        get => _selectedItem;
        set => SetField(ref _selectedItem, value);
    }

    public WithGroupAndFilterViewModel()
    {
        var testData = TestDataLoader.LoadTestData(100, 500);

        this.Items = new DataGridCollectionView(testData, false, false);
        this.Items.GroupDescriptions.Add(new DataGridPathGroupDescription(nameof(TestDataRow.Country)));
        this.Items.Filter = x => ((TestDataRow) x).Status == true;
        this.SelectedItem = testData.FirstOrDefault();
    }
}

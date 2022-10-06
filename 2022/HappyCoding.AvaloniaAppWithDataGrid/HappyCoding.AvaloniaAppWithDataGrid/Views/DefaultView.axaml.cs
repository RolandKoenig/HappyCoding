using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace HappyCoding.AvaloniaAppWithDataGrid.Views;

public partial class DefaultView : UserControl
{
    public DefaultView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnCmdResetSorting_Click(object? sender, RoutedEventArgs e)
    {
        var dataGrid = this.FindControl<DataGrid>("CtrlDataGrid");
        foreach (var actColumn in dataGrid.Columns)
        {
            actColumn.ClearSort();
        }
    }
}
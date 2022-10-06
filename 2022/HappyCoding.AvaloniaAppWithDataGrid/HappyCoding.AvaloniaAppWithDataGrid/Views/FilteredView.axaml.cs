using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HappyCoding.AvaloniaAppWithDataGrid.Views;

public partial class FilteredView : UserControl
{
    public FilteredView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
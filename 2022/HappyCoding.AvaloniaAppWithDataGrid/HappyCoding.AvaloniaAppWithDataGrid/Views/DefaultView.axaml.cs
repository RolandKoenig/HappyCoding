using Avalonia.Controls;
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
}
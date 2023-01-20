using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HappyCoding.CommunityToolkitMvvm;

public partial class SelectedDataRowView : UserControl
{
    public SelectedDataRowView()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
using Microsoft.UI.Xaml.Controls;

namespace HappyCoding.SimpleWinUI3App.Pages;

public sealed partial class SearchGrid : Page
{
    public SearchGridViewModel ViewModel { get; } = new SearchGridViewModel();

    public SearchGrid()
    {
        this.InitializeComponent();
    }
}

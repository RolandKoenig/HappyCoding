using Windows.ApplicationModel;
using Microsoft.UI.Xaml.Controls;

namespace HappyCoding.SimpleWinUI3App.Pages;

public sealed partial class SearchGrid : Page
{
    public SearchGridViewModel? ViewModel { get; }

    public SearchGrid()
    {
        this.InitializeComponent();

        if (!DesignMode.DesignModeEnabled)
        {
            ViewModel = App.SetupViewModel<SearchGridViewModel>(this);
        }
    }
}

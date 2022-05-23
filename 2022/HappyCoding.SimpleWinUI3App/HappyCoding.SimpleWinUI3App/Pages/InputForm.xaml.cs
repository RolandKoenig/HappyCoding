using Windows.ApplicationModel;
using Microsoft.UI.Xaml.Controls;

namespace HappyCoding.SimpleWinUI3App.Pages;

public sealed partial class InputForm : Page
{
    public InputFormViewModel? ViewModel { get; }

    public InputForm()
    {
        this.InitializeComponent();

        if (!DesignMode.DesignModeEnabled)
        {
            ViewModel = App.SetupViewModel<InputFormViewModel>(this);
        }
    }
}

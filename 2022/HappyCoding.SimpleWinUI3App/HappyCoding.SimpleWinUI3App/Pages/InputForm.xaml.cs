using Microsoft.UI.Xaml.Controls;

namespace HappyCoding.SimpleWinUI3App.Pages;

public sealed partial class InputForm : Page
{
    public InputFormViewModel ViewModel { get; } = new InputFormViewModel();

    public InputForm()
    {
        this.InitializeComponent();
    }
}

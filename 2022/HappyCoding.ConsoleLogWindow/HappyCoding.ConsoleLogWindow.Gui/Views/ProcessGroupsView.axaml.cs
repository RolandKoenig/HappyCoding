using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HappyCoding.ConsoleLogWindow.Gui.Util;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;
public partial class ProcessGroupsView : UserControl
{
    public ProcessGroupsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        this.CreateAndAttachViewModel<ProcessGroupsViewModel>();
    }
}

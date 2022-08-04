using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Gui.Util;

namespace HappyCoding.ConsoleLogWindow.Gui.Views.ProcessGroups;
public partial class ProcessStatusView : UserControl
{
    public static readonly DirectProperty<ProcessStatusView, ProcessInfo?> SelectedProcessProperty =
        AvaloniaProperty.RegisterDirect<ProcessStatusView, ProcessInfo?>(
            nameof(SelectedProcess),
            control => control.SelectedProcess,
            (control, val) => control.SelectedProcess = val);

    public ProcessInfo? SelectedProcess
    {
        get => (this.DataContext as ProcessStatusViewModel)?.SelectedProcess;
        set
        {
            if (this.DataContext is ProcessStatusViewModel viewModel)
            {
                var oldValue = viewModel.SelectedProcess;
                viewModel.SelectedProcess = value;

                this.RaisePropertyChanged(SelectedProcessProperty, oldValue, viewModel.SelectedProcess);
            }
        }
    }

    public ProcessStatusView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        this.CreateAndAttachViewModel<ProcessStatusViewModel>();
    }
}

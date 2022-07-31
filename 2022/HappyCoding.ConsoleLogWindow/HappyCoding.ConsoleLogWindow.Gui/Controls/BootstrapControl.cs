using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;

namespace HappyCoding.ConsoleLogWindow.Gui.Controls;

public class BootstrapControl : Grid
{
    public static readonly DirectProperty<BootstrapControl, ICommand?> BootstrapCommandProperty =
        AvaloniaProperty.RegisterDirect<BootstrapControl, ICommand?>(
            nameof(BootstrapCommand),
            control => control.BootstrapCommand,
            (control, val) => control.BootstrapCommand = val);

    private ICommand? _bootstrapCommand;
    private bool _bootstrapCommandCalled;

    public ICommand? BootstrapCommand
    {
        get { return _bootstrapCommand; }
        private set { this.SetAndRaise(BootstrapCommandProperty, ref _bootstrapCommand, value); }
    }

    /// <inheritdoc />
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (Design.IsDesignMode) { return; }

        if (!_bootstrapCommandCalled)
        {
            _bootstrapCommandCalled = true;
            _bootstrapCommand?.Execute(null);
        }
    }
}

using System.Collections.ObjectModel;
using HappyCoding.ConsoleLogWindow.Domain.Model;
using HappyCoding.ConsoleLogWindow.Domain.Ports;
using HappyCoding.ConsoleLogWindow.Gui.Util;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

public class RunningProcessViewModel : ViewModelBase
{
    private readonly IProcessRunner _processRunner;

    public ObservableCollection<ProcessOutputLine> ProcessOutput { get; private set; }

    public RunningProcessViewModel(
        IProcessRunner processRunner)
    {
        _processRunner = processRunner;
    }

    /// <inheritdoc />
    public override void ViewLoaded(IView view)
    {
        base.ViewLoaded(view);
    }

    /// <inheritdoc />
    public override void ViewUnloaded()
    {
        base.ViewUnloaded();
    }
}

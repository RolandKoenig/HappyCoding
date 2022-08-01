using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Events;

[FirLibMessage]
public record ProcessInfoSelectionChangedEvent
{
    public ProcessInfo? SelectedProcessOld { get; init; }

    public ProcessInfo? SelectedProcessNew { get; init; }
}

using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Messages;

[FirLibMessage]
public record ProcessInfoSelectionChangedMessage
{
    public ProcessInfo? SelectedProcessOld { get; init; }

    public ProcessInfo? SelectedProcessNew { get; init; }
}

using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.Messages;

[FirLibMessage]
public record ProcessStartedMessage
{
    public ProcessInfo ProcessInfo { get; init; }

    public IRunningProcess Process { get; init; }
}

using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.Messages;

[FirLibMessage]
public record ProcessStoppedMessage
{
    public ProcessInfo ProcessInfo { get; init; }
}

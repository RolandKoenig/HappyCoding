using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Application.Events;

[FirLibMessage]
public record ProcessStoppedEvent(ProcessInfo ProcessInfo);
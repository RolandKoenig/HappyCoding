using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Application.Runtime;

public interface IRunningProcess : IDisposable
{
    IReadOnlyList<ProcessOutputLine> Output { get; }

    bool IsRunning { get; }
}
using HappyCoding.ConsoleLogWindow.Domain.Model;

namespace HappyCoding.ConsoleLogWindow.Domain.Runtime;

public interface IRunningProcess : IDisposable
{
    IReadOnlyList<ProcessOutputLine> Output { get; }

    bool IsRunning { get; }
}
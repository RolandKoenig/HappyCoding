
using HappyCoding.ConsoleLogWindow.Domain.Model;
using HappyCoding.ConsoleLogWindow.Domain.Runtime;

namespace HappyCoding.ConsoleLogWindow.Domain.Ports;

public interface IProcessRunner
{
    Task<IRunningProcess> StartProcessAsync(ProcessInfo processInfo);

    Task<bool> IsProcessRunning(ProcessInfo processInfo);

    Task<IRunningProcess?> TryGetRunningProcess(ProcessInfo processInfo);
}

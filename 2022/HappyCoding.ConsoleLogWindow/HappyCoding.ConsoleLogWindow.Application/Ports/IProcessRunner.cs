
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Runtime;

namespace HappyCoding.ConsoleLogWindow.Application.Ports;

public interface IProcessRunner
{
    Task<IRunningProcess> StartProcessAsync(ProcessInfo processInfo);

    Task<bool> IsProcessRunningAsync(ProcessInfo processInfo);

    Task<IRunningProcess?> TryGetRunningProcessAsync(ProcessInfo processInfo);
}

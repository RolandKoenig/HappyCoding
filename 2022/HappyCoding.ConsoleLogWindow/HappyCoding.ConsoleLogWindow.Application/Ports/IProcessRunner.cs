
using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.Application.Ports;

public interface IProcessRunner
{
    Task<IRunningProcess> StartProcessAsync(ProcessInfo processInfo);

    Task StopProcessAsync(ProcessInfo processInfo);

    Task<bool> IsProcessRunningAsync(ProcessInfo processInfo);

    Task<IRunningProcess?> TryGetRunningProcessAsync(ProcessInfo processInfo);
}

using System.Diagnostics;
using HappyCoding.ConsoleLogWindow.Domain.Exceptions;
using HappyCoding.ConsoleLogWindow.Domain.Model;
using HappyCoding.ConsoleLogWindow.Domain.Ports;
using HappyCoding.ConsoleLogWindow.Domain.Runtime;

namespace HappyCoding.ConsoleLogWindow.StdOutProcessRunner;

internal class StdOutProcessRunnerImpl : IProcessRunner
{
    private Dictionary<ProcessInfo, StdOutRunningProcess> _runningProcesses;

    public StdOutProcessRunnerImpl()
    {
        _runningProcesses = new Dictionary<ProcessInfo, StdOutRunningProcess>();
    }

    /// <inheritdoc />
    public Task<IRunningProcess> StartProcessAsync(ProcessInfo processInfo)
    {
        var startInfo = new ProcessStartInfo(processInfo.CommandLine);
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardInput = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        var process = Process.Start(startInfo);
        if (process == null)
        {
            throw new ConsoleLogWindowAdapterException(
                nameof(StdOutProcessRunnerImpl),
                $"Unable to start process '{processInfo.CommandLine}'");
        }

        var runningProcess = new StdOutRunningProcess(process);
        _runningProcesses[processInfo] = runningProcess;

        return Task.FromResult((IRunningProcess)runningProcess);
    }

    /// <inheritdoc />
    public Task<bool> IsProcessRunning(ProcessInfo processInfo)
    {
        return Task.FromResult(
            _runningProcesses.ContainsKey(processInfo));
    }

    /// <inheritdoc />
    public Task<IRunningProcess?> TryGetRunningProcess(ProcessInfo processInfo)
    {
        if (_runningProcesses.TryGetValue(processInfo, out var runningProcess))
        {
            return Task.FromResult((IRunningProcess?) runningProcess);
        }
        else
        {
            return Task.FromResult((IRunningProcess?) null);
        }
    }
}
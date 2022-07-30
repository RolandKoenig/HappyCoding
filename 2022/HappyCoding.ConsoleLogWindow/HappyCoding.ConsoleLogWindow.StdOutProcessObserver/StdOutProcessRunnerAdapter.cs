using System.Diagnostics;
using HappyCoding.ConsoleLogWindow.Application.Exceptions;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;

namespace HappyCoding.ConsoleLogWindow.StdOutProcessRunner;

internal class StdOutProcessRunnerAdapter : IProcessRunner
{
    private Dictionary<ProcessInfo, StdOutRunningProcess> _runningProcesses;

    public StdOutProcessRunnerAdapter()
    {
        _runningProcesses = new Dictionary<ProcessInfo, StdOutRunningProcess>();
    }

    /// <inheritdoc />
    public Task<IRunningProcess> StartProcessAsync(ProcessInfo processInfo)
    {
        var startInfo = new ProcessStartInfo(
            processInfo.FileName, 
            processInfo.Arguments);
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardInput = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        var process = Process.Start(startInfo);
        if (process == null)
        {
            throw new ConsoleLogWindowAdapterException(
                nameof(StdOutProcessRunnerAdapter),
                $"Unable to start process '{processInfo.FileName}'");
        }

        var runningProcess = new StdOutRunningProcess(process);
        _runningProcesses[processInfo] = runningProcess;

        return Task.FromResult((IRunningProcess)runningProcess);
    }

    /// <inheritdoc />
    public async Task StopProcessAsync(ProcessInfo processInfo)
    {
        var process = (StdOutRunningProcess?)(await TryGetRunningProcessAsync(processInfo));
        if (process == null)
        {
            return;
        }
        
        process.Kill();
        process.Dispose();

        _runningProcesses.Remove(processInfo);
    }

    /// <inheritdoc />
    public Task<bool> IsProcessRunningAsync(ProcessInfo processInfo)
    {
        return Task.FromResult(
            _runningProcesses.ContainsKey(processInfo));
    }

    /// <inheritdoc />
    public Task<IRunningProcess?> TryGetRunningProcessAsync(ProcessInfo processInfo)
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
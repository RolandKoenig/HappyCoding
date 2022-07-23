using System.Diagnostics;
using HappyCoding.ConsoleLogWindow.Domain.Exceptions;
using HappyCoding.ConsoleLogWindow.Domain.Model;
using HappyCoding.ConsoleLogWindow.Domain.Ports;
using HappyCoding.ConsoleLogWindow.Domain.Runtime;

namespace HappyCoding.ConsoleLogWindow.StdOutProcessRunner;

internal class StdOutProcessRunnerImpl : IProcessRunner
{
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

        return Task.FromResult((IRunningProcess)new StdOutRunningProcess(process));
    }
}
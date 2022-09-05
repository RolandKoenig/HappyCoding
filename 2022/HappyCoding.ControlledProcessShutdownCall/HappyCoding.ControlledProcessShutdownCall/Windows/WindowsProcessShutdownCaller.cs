using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace HappyCoding.ControlledProcessShutdownCall.Windows;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal class WindowsProcessShutdownCaller : IProcessShutdownCaller
{
    public async Task EndProcessAsync(Process process, CancellationToken cancellationToken)
    {
        var processID = process.Id;

        var startInfo = new ProcessStartInfo(
            "dotnet", $"HappyCoding.WinProcessSignalingHelper.dll {processID}");
        startInfo.RedirectStandardOutput = true;

        var helperProcess = Process.Start(startInfo);
        await helperProcess!.WaitForExitAsync(cancellationToken);
        var exitCode = helperProcess.ExitCode;

        if (exitCode == 0)
        {
            await process.WaitForExitAsync(cancellationToken);
        }
        else
        {
            var error = (await helperProcess.StandardOutput.ReadToEndAsync()) ?? "";
            throw new UnableToEndProcessException(
                $"Unable to end process {processID}, helper process returned {exitCode} ({error})");
        }
    }
}

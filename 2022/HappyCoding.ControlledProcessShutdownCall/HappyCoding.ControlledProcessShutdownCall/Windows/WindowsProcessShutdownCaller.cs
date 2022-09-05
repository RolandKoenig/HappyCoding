using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace HappyCoding.ControlledProcessShutdownCall.Windows;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal class WindowsProcessShutdownCaller : IProcessShutdownCaller
{
    public async Task StopProcessAsync(Process processToStop, CancellationToken cancellationToken)
    {
        if (processToStop.CloseMainWindow())
        {
            // Shortcut for UI processes
            await processToStop.WaitForExitAsync(cancellationToken);
        }
        else
        {
            // Start helper process which sends CTRL+C command
            var startInfo = new ProcessStartInfo(
                "dotnet", $"HappyCoding.WinProcessSignalingHelper.dll {processToStop.Id}");
            startInfo.RedirectStandardOutput = true;

            var helperProcess = Process.Start(startInfo);
            await helperProcess!.WaitForExitAsync(cancellationToken);
            var exitCode = helperProcess.ExitCode;

            if (exitCode == 0)
            {
                await processToStop.WaitForExitAsync(cancellationToken);
            }
            else
            {
                var error = (await helperProcess.StandardOutput.ReadToEndAsync()) ?? "";
                throw new UnableToEndProcessException(
                    $"Unable to end process {processToStop.Id}, helper process returned {exitCode} ({error})");
            }
        }
    }
}

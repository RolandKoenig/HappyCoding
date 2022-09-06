using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace HappyCoding.ControlledProcessShutdownCall.Windows;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal class WindowsProcessStopCaller : IProcessStopCaller
{
    public async Task StopProcessAsync(Process processToStop, CancellationToken cancellationToken)
    {
        

        if (processToStop.MainWindowHandle != IntPtr.Zero)
        {
            // Trigger closing of the MainWindow
            if (processToStop.CloseMainWindow())
            {
                await processToStop.WaitForExitAsync(cancellationToken);
            }
            else
            {
                throw new UnableToStopProcessException(
                    $"Unable to stop process {processToStop.Id}, CloseMainWindow returned false");
            }
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
                throw new UnableToStopProcessException(
                    $"Unable to end process {processToStop.Id}, helper process returned {exitCode} ({error})");
            }
        }
    }
}

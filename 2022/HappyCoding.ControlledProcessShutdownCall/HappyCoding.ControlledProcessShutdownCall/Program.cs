using System.Diagnostics;
using HappyCoding.ControlledProcessShutdownCall.Unix;
using HappyCoding.ControlledProcessShutdownCall.Windows;

namespace HappyCoding.ControlledProcessShutdownCall;

internal class Program
{
    static async Task<int> Main()
    {
        try
        {
            IProcessStopCaller processStopCaller;
            if (OperatingSystem.IsWindows())
            {
                processStopCaller = new WindowsProcessStopCaller();
            }
            else if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
            {
                processStopCaller = new UnixProcessStopCaller();
            }
            else
            {
                Console.WriteLine("Current OS not supported!");
                return -1;
            }

            var processStartInfo = new ProcessStartInfo(
                "dotnet", "HappyCoding.ControlledProcessShutdownCall.DummyProcess.dll");
            processStartInfo.UseShellExecute = true;
            // var processStartInfo = new ProcessStartInfo("Notepad.exe");
            var process = Process.Start(processStartInfo)!;
            Console.WriteLine("Process tarted...");

            await Task.Delay(2000);

            Console.WriteLine("Signaling process");
            await processStopCaller.StopProcessAsync(process, CancellationToken.None);

            Console.WriteLine("Process ended");

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }
    }
}

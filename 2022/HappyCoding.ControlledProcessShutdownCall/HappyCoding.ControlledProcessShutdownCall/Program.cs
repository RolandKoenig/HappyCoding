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
            IProcessShutdownCaller processShutdownCaller;
            if (OperatingSystem.IsWindows())
            {
                processShutdownCaller = new WindowsProcessShutdownCaller();
            }
            else if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
            {
                processShutdownCaller = new UnixProcessShutdownCaller();
            }
            else
            {
                return -1;
            }

            var processStartInfo = new ProcessStartInfo(
                "dotnet", "HappyCoding.ControlledProcessShutdownCall.DummyProcess.dll");
            processStartInfo.UseShellExecute = true;
            var process = Process.Start(processStartInfo)!;
            Console.WriteLine("Process tarted...");

            await Task.Delay(2000);

            Console.WriteLine("Signaling process");
            await processShutdownCaller.EndProcessAsync(process, CancellationToken.None);

            Console.WriteLine("Process ended");
            Console.ReadLine();

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }
    }
}

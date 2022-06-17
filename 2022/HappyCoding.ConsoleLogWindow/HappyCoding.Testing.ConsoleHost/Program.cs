using System.Diagnostics;

namespace HappyCoding.Testing.ConsoleHost;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Starting...");

        var startInfo = new ProcessStartInfo("HappyCoding.ConsoleLogWindow.TestService.exe");
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardInput = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        var process = Process.Start(startInfo);
        process.BeginErrorReadLine();
        process.BeginOutputReadLine();
        process.OutputDataReceived += (_, eArgs) => Console.WriteLine("Output: " + eArgs.Data);
        process.ErrorDataReceived += (_, eArgs) => Console.WriteLine("Error: " + eArgs.Data);

        Console.ReadLine();

        process.Kill();
    }
}
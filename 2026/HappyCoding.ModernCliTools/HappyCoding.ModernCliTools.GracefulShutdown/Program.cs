using System.Runtime.InteropServices;

namespace HappyCoding.ModernCliTools.GracefulShutdown;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Handle SIGINT
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        PosixSignalRegistration.Create(PosixSignal.SIGINT, context =>
        {
            // We do want to gracefully shutdown
            context.Cancel = true;
            
            // Trigger gracefull shutdown
            Console.WriteLine("Trigger graceful shutdown");
            cancellationTokenSource.Cancel();
        });

        // Simulate processing
        Console.WriteLine("Start processing...");
        try
        {
            await Task.Delay(50000, cancellationToken);
            Console.WriteLine("Finished processing");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Canceled");
        }
    }
}
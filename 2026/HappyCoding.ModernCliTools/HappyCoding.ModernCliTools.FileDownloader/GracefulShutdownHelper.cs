using System.Runtime.InteropServices;

namespace HappyCoding.ModernCliTools.FileDownloader;

public static class GracefulShutdownHelper
{
    public static CancellationToken CreateCancellationToken()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        PosixSignalRegistration.Create(PosixSignal.SIGINT, context =>
        {
            // We do want to gracefully shutdown
            context.Cancel = true;
            
            // Trigger gracefull shutdown
            cancellationTokenSource.Cancel();
        });
        return cancellationTokenSource.Token;
    }
}
using System.Diagnostics;

namespace HappyCoding.ControlledProcessShutdownCall;

internal interface IProcessStopCaller
{
    Task StopProcessAsync(Process processToStop, CancellationToken cancellationToken);
}

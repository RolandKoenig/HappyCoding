using System.Diagnostics;

namespace HappyCoding.ControlledProcessShutdownCall;

internal interface IProcessShutdownCaller
{
    Task StopProcessAsync(Process processToStop, CancellationToken cancellationToken);
}

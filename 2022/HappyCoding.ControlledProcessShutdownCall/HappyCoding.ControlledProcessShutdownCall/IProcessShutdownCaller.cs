using System.Diagnostics;

namespace HappyCoding.ControlledProcessShutdownCall;

internal interface IProcessShutdownCaller
{
    Task EndProcessAsync(Process process, CancellationToken cancellationToken);
}

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Mono.Unix.Native;

namespace HappyCoding.ControlledProcessShutdownCall.Unix;

[SupportedOSPlatform(nameof(OSPlatform.Linux))]
[SupportedOSPlatform(nameof(OSPlatform.OSX))]
internal class UnixProcessShutdownCaller : IProcessShutdownCaller
{
    public Task StopProcessAsync(Process processToStop, CancellationToken cancellationToken)
    {
        Syscall.kill(processToStop.Id, Signum.SIGINT);

        return processToStop.WaitForExitAsync(cancellationToken);
    }
}

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Mono.Unix.Native;

namespace HappyCoding.ControlledProcessShutdownCall.Unix;

[SupportedOSPlatform(nameof(OSPlatform.Linux))]
[SupportedOSPlatform(nameof(OSPlatform.OSX))]
internal class UnixProcessShutdownCaller : IProcessShutdownCaller
{
    public Task EndProcessAsync(Process process, CancellationToken cancellationToken)
    {
        Syscall.kill(process.Id, Signum.SIGINT);


        return process.WaitForExitAsync(cancellationToken);
    }
}

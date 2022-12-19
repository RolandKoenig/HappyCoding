using System.Threading;
using System.Threading.Tasks;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

public interface ITestChannel
{
    bool IsConnected { get; }

    ulong CountSuccess { get; }

    ulong CountErrors { get; }

    public string LastErrorDetails { get; }

    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);
}

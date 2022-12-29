using System.Threading;
using System.Threading.Tasks;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

public interface ITestChannel
{
    bool IsConnected { get; }

    ulong CountSuccess { get; }

    ulong CountSpikes { get; }

    ulong CountTimeouts { get; }

    ulong CountErrors { get; }

    double CallDurationMinMS { get; }

    double CallDurationAvgMS { get; }

    double CallDurationMaxMS { get; }

    double CallsPerSecond { get; }

    string LastErrorDetails { get; }

    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);

    void ResetMetrics();
}

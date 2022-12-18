using System.Threading;
using System.Threading.Tasks;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

public interface ITestChannel
{
    bool IsConnected { get; }

    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);
}

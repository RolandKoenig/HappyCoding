using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RolandK.Utils.Collections;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

public abstract class BaseChannel : ITestChannel
{
    private ulong _countSuccess;
    private ulong _countTimeouts;
    private ulong _countErrors;
    private string _lastErrorDetails = string.Empty;
    private RingBuffer<double> _callDurationsMS = new(32);

    /// <inheritdoc />
    public abstract bool IsConnected { get; }

    /// <inheritdoc />
    public ulong CountSuccess => _countSuccess;

    /// <inheritdoc />
    public ulong CountTimeouts => _countTimeouts;

    /// <inheritdoc />
    public ulong CountErrors => _countErrors;

    /// <inheritdoc />
    public double CallDurationMinMS => _callDurationsMS.Count > 0 ? _callDurationsMS.Min() : 0;

    /// <inheritdoc />
    public double CallDurationAvgMS => _callDurationsMS.Count > 0 ? _callDurationsMS.Average() : 0;

    /// <inheritdoc />
    public double CallDurationMaxMS => _callDurationsMS.Count > 0 ? _callDurationsMS.Max() : 0;

    /// <inheritdoc />
    public string LastErrorDetails => _lastErrorDetails;

    protected void NotifySuccess(double callDurationMS)
    {
        Interlocked.Increment(ref _countSuccess);

        _callDurationsMS.Add(callDurationMS);
    }

    protected void NotifyTimeout()
    {
        Interlocked.Increment(ref _countTimeouts);
    }

    protected void NotifyError(string? errorDetails = null)
    {
        Interlocked.Increment(ref _countErrors);

        if (!string.IsNullOrEmpty(errorDetails))
        {
            _lastErrorDetails = errorDetails;
        }
    }

    /// <inheritdoc />
    public abstract Task StartAsync(CancellationToken cancellationToken);

    /// <inheritdoc />
    public abstract Task StopAsync(CancellationToken cancellationToken);
}

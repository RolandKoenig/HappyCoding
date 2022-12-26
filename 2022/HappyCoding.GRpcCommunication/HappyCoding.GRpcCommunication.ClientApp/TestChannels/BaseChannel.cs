using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RolandK.Utils.Collections;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

public abstract class BaseChannel : ITestChannel
{
    private const int CACHED_CALL_INFO_COUNT_FOR_METRICS = 32;

    private ulong _countSuccess;
    private ulong _countTimeouts;
    private ulong _countErrors;
    private string _lastErrorDetails = string.Empty;

    private RingBuffer<double> _callDurationsMS = new(CACHED_CALL_INFO_COUNT_FOR_METRICS);
    private RingBuffer<DateTimeOffset> _callTimestamps = new(CACHED_CALL_INFO_COUNT_FOR_METRICS);
    private object _callMetricRingBufferLock = new object();

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

    public double CallsPerSecond
    {
        get
        {
            var first = DateTimeOffset.MinValue;
            var last = DateTimeOffset.MinValue;
            var callCount = 0;
            foreach (var actCallTimestamp in _callTimestamps)
            {
                callCount++;

                if (first == DateTimeOffset.MinValue)
                {
                    first = actCallTimestamp;
                    continue;
                }
                last = actCallTimestamp;
            }

            if (callCount <= 2) { return 0; }
            else if (last == first) { return 0; }
            else if (last < first) { return 0; }
            {
                return callCount / (last - first).TotalSeconds;
            }
        }
    }

    /// <inheritdoc />
    public string LastErrorDetails => _lastErrorDetails;

    protected void NotifySuccess(double callDurationMS)
    {
        Interlocked.Increment(ref _countSuccess);

        lock (_callMetricRingBufferLock)
        {
            _callDurationsMS.Add(callDurationMS);
            _callTimestamps.Add(DateTimeOffset.UtcNow);
        }
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

    /// <inheritdoc />
    public void ResetMetrics()
    {
        _countErrors = 0;
        _countSuccess = 0;
        _countTimeouts = 0;

        lock (_callMetricRingBufferLock)
        {
            _callDurationsMS = new RingBuffer<double>(CACHED_CALL_INFO_COUNT_FOR_METRICS);
            _callTimestamps = new RingBuffer<DateTimeOffset>(CACHED_CALL_INFO_COUNT_FOR_METRICS);
        }

        _lastErrorDetails = string.Empty;
    }
}

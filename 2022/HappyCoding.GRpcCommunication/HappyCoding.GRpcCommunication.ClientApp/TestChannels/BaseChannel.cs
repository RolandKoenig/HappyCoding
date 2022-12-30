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
    private ulong _countSpikes;
    private ulong _countTimeouts;
    private ulong _countErrors;
    private string _lastErrorDetails = string.Empty;

    private RingBuffer<double>[] _callDurationMS;
    private RingBuffer<DateTimeOffset>[] _callTimestamps;

    /// <inheritdoc />
    public abstract bool IsConnected { get; }

    /// <inheritdoc />
    public ulong CountSuccess => _countSuccess;

    public ulong CountSpikes => _countSpikes;

    /// <inheritdoc />
    public ulong CountTimeouts => _countTimeouts;

    /// <inheritdoc />
    public ulong CountErrors => _countErrors;

    /// <inheritdoc />
    public double CallDurationMinMS
    {
        get
        {
            var callDurations = _callDurationMS;
            if (!callDurations.Any(x => x.Count > 0)) { return 0; }

            return callDurations.GetFullEnumerable().Min();
        }
    }

    /// <inheritdoc />
    public double CallDurationAvgMS
    {
        get
        {
            var callDurations = _callDurationMS;
            if (!callDurations.Any(x => x.Count > 0)) { return 0; }

            return callDurations.GetFullEnumerable().Average();
        }
    }

    /// <inheritdoc />
    public double CallDurationMaxMS
    {
        get
        {
            var callDurations = _callDurationMS;
            if (!callDurations.Any(x => x.Count > 0)) { return 0; }

            return callDurations.GetFullEnumerable().Max();
        }
    }

    public double CallsPerSecond
    {
        get
        {
            var first = DateTimeOffset.MinValue;
            var last = DateTimeOffset.MinValue;
            var callCount = 0;
            foreach (var actCallTimestampsCollection in _callTimestamps)
            {
                foreach (var actCallTimestamp in actCallTimestampsCollection)
                {
                    callCount++;

                    if (first == DateTimeOffset.MinValue)
                    {
                        first = actCallTimestamp;
                        continue;
                    }
                    last = actCallTimestamp;
                }
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

    protected BaseChannel()
    {
        _callTimestamps = new RingBuffer<DateTimeOffset>[ClientAppConstants.MAX_PARALLEL_CALLS];
        _callDurationMS = new RingBuffer<double>[ClientAppConstants.MAX_PARALLEL_CALLS];
        for (var loop = 0; loop < ClientAppConstants.MAX_PARALLEL_CALLS; loop++)
        {
            _callTimestamps[loop] = new RingBuffer<DateTimeOffset>(CACHED_CALL_INFO_COUNT_FOR_METRICS);
            _callDurationMS[loop] = new RingBuffer<double>(CACHED_CALL_INFO_COUNT_FOR_METRICS);
        }
    }

    protected void NotifySuccess(int threadId, double callDurationMS, bool isSpike)
    {
        if (threadId < 0) { return; }
        if (threadId >= ClientAppConstants.MAX_PARALLEL_CALLS) { return; }

        Interlocked.Increment(ref _countSuccess);

        _callDurationMS[threadId].Add(callDurationMS);
        _callTimestamps[threadId].Add(DateTimeOffset.UtcNow);

        if (isSpike)
        {
            Interlocked.Increment(ref _countSpikes);
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
        _countSpikes = 0;
        _countTimeouts = 0;

        var callTimestamps = new RingBuffer<DateTimeOffset>[ClientAppConstants.MAX_PARALLEL_CALLS];
        var callDurationMS = new RingBuffer<double>[ClientAppConstants.MAX_PARALLEL_CALLS];
        for (var loop = 0; loop < ClientAppConstants.MAX_PARALLEL_CALLS; loop++)
        {
            callTimestamps[loop] = new RingBuffer<DateTimeOffset>(CACHED_CALL_INFO_COUNT_FOR_METRICS);
            callDurationMS[loop] = new RingBuffer<double>(CACHED_CALL_INFO_COUNT_FOR_METRICS);
        }
        _callTimestamps = callTimestamps;
        _callDurationMS = callDurationMS;

        _lastErrorDetails = string.Empty;
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.Services;
using RolandK.Utils.Collections;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

internal class SimpleRequestReplyWithStringChannel : ITestChannel
{
    private GrpcChannel? _channel;
    private ulong _countSuccess;
    private ulong _countTimeouts;
    private ulong _countErrors;
    private string _lastErrorDetails = string.Empty;
    private RingBuffer<double> _callDurationsMS = new(32);

    /// <inheritdoc />
    public bool IsConnected => _channel?.State == ConnectivityState.Ready;

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

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _callDurationsMS.Clear();

        var options = await ClientOptions.LoadAsync(cancellationToken);

        var protocol = options.UseHttps ? "https" : "http";

        _channel = GrpcChannel.ForAddress($"{protocol}://{options.TargetHost}:{options.Port}");

        this.Run(_channel, options.DelayBetweenCallsMS, options.CallTimeoutMS);
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        var channel = _channel;
        _channel = null;

        channel?.Dispose();

        return Task.CompletedTask;
    }

    private async void Run(GrpcChannel channel, ushort delayBetweenCallsMS, uint callTimeoutMS)
    {
        await Task.Delay(delayBetweenCallsMS)
            .ConfigureAwait(false);

        while (channel == _channel)
        {
            try
            {
                var stopWatch = Stopwatch.StartNew();

                var client = new SimpleRequestReplyWithStringHandler.SimpleRequestReplyWithStringHandlerClient(channel);
                client.Handle(
                    new SimpleRequestWithString() {Name = "Test"},
                    new CallOptions(deadline: DateTime.UtcNow.AddMilliseconds(callTimeoutMS)));

                _callDurationsMS.Add(stopWatch.Elapsed.TotalMilliseconds);

                Interlocked.Increment(ref _countSuccess);
            }
            catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.DeadlineExceeded)
            {
                if (channel == _channel)
                {
                    Interlocked.Increment(ref _countTimeouts);
                }
            }
            catch (Exception ex)
            {
                if (channel == _channel)
                {
                    Interlocked.Increment(ref _countErrors);
                    _lastErrorDetails = ex.ToString();
                }
            }

            await Task.Delay(delayBetweenCallsMS)
                .ConfigureAwait(false);
        }
    }
}

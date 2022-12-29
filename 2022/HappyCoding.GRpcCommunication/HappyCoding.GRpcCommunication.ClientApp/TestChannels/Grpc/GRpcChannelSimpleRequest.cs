using System;
using System.Diagnostics;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.SimpleRequest;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Grpc;

internal class GrpcChannelSimpleRequest : BaseChannel
{
    private GrpcChannel? _channel;

    /// <inheritdoc />
    public override bool IsConnected => _channel?.State == ConnectivityState.Ready;

    /// <inheritdoc />
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var options = await ClientOptions.LoadAsync(cancellationToken);

        try
        {
            _channel = GrpcHelper.BuildChannel(options);
            if (options.ConnectGrpcAtStart)
            {
                await _channel.ConnectAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            base.NotifyError(ex.ToString());
            _channel = null;

            throw;
        }

        this.Run(0, _channel, options.DelayBetweenCallsMS, options.SpikeThresholdMS, options.CallTimeoutMS);
    }

    /// <inheritdoc />
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        var channel = _channel;
        _channel = null;

        channel?.Dispose();

        return Task.CompletedTask;
    }

    private async void Run(int threadId, GrpcChannel channel, ushort delayBetweenCallsMS, uint spikeThresholdMS, uint callTimeoutMS)
    {
        await Task.Delay(100)
            .ConfigureAwait(false);

        while (channel == _channel)
        {
            try
            {
                var requestObj = new SimpleRequest()
                {
                    Name = "Test"
                };

                var stopWatch = Stopwatch.StartNew();

                var client = new SimpleRequestHandler.SimpleRequestHandlerClient(channel);
                client.Handle(
                    requestObj,
                    new CallOptions(deadline: DateTime.UtcNow.AddMilliseconds(callTimeoutMS)));

                var totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
                base.NotifySuccess(threadId, totalMilliseconds, totalMilliseconds > spikeThresholdMS);
            }
            catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.DeadlineExceeded)
            {
                if (channel == _channel)
                {
                    base.NotifyTimeout();
                }
            }
            catch (Exception ex)
            {
                if (channel == _channel)
                {
                    base.NotifyError(ex.ToString());
                }
            }

            if (delayBetweenCallsMS > 0)
            {
                await Task.Delay(delayBetweenCallsMS)
                    .ConfigureAwait(false);
            }
        }
    }
}

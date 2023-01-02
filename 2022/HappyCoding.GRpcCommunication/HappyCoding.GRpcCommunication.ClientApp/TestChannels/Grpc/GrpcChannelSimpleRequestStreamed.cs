using System;
using System.Diagnostics;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.SimpleRequest;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Grpc;

internal class GrpcChannelSimpleRequestStreamed : BaseChannel
{
    private GrpcChannel? _channel;
    private bool _lastCallSuccessful;

    /// <inheritdoc />
    public override bool IsConnected => (_channel?.State == ConnectivityState.Ready) && 
                                        (_lastCallSuccessful);

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
                _lastCallSuccessful = true;
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

    private async void Run(
        int threadId, 
        GrpcChannel channel,
        ushort delayBetweenCallsMS, uint spikeThresholdMS, uint callTimeoutMS)
    {
        await Task.Delay(100)
            .ConfigureAwait(false);

        while (channel == _channel)
        {
            AsyncDuplexStreamingCall<SimpleRequest, SimpleResponse>? stream = null;
            if (stream == null)
            {
                var client = new SimpleRequestHandler.SimpleRequestHandlerClient(_channel);
                stream = client.HandleStreamed();
            }

            try
            {
                var requestObj = new SimpleRequest()
                {
                    Name = "Test"
                };

                var stopWatch = Stopwatch.StartNew();

                var nextResponseTask = stream.ResponseStream.WaitForResponse(CancellationToken.None);
                await stream.RequestStream.WriteAsync(requestObj);
                var response = await nextResponseTask;

                _lastCallSuccessful = true;

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
                    _lastCallSuccessful = false;
                }

                stream.Dispose();
                stream = null;
            }

            if (delayBetweenCallsMS > 0)
            {
                await Task.Delay(delayBetweenCallsMS)
                    .ConfigureAwait(false);
            }
        }
    }
}

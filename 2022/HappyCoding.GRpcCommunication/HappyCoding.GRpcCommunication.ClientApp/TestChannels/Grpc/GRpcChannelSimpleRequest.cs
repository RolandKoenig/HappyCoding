using System;
using System.Diagnostics;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.SimpleRequest;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Grpc;

internal class GRpcChannelSimpleRequest : BaseChannel
{
    private GrpcChannel? _channel;

    /// <inheritdoc />
    public override bool IsConnected => _channel?.State == ConnectivityState.Ready;

    /// <inheritdoc />
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var options = await ClientOptions.LoadAsync(cancellationToken);

        var protocol = options.UseHttps ? "https" : "http";

        _channel = GrpcChannel.ForAddress($"{protocol}://{options.TargetHost}:{options.PortHttp2}");

        Run(_channel, options.DelayBetweenCallsMS, options.CallTimeoutMS);
    }

    /// <inheritdoc />
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        var channel = _channel;
        _channel = null;

        channel?.Dispose();

        return Task.CompletedTask;
    }

    private async void Run(GrpcChannel channel, ushort delayBetweenCallsMS, uint callTimeoutMS)
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

                NotifySuccess(stopWatch.Elapsed.TotalMilliseconds);
            }
            catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.DeadlineExceeded)
            {
                if (channel == _channel)
                {
                    NotifyTimeout();
                }
            }
            catch (Exception ex)
            {
                if (channel == _channel)
                {
                    NotifyError(ex.ToString());
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

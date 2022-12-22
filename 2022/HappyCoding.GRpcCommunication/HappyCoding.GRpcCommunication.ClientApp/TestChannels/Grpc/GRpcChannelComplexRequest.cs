using System;
using System.Diagnostics;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.ComplexRequest;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Grpc;

internal class GRpcChannelComplexRequest : BaseChannel
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
        if (options.ConnectGrpcAtStart)
        {
            await _channel.ConnectAsync(cancellationToken);
        }

        this.Run(_channel, options.DelayBetweenCallsMS, options.CallTimeoutMS);
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
        var random = new Random(100);

        await Task.Delay(100)
            .ConfigureAwait(false);

        while (channel == _channel)
        {
            try
            {
                var requestObj = BuildComplexRequest(random);

                var stopWatch = Stopwatch.StartNew();

                var client = new ComplexRequestHandler.ComplexRequestHandlerClient(channel);
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

    private static ComplexRequest BuildComplexRequest(Random random)
    {
        var result = new ComplexRequest();
        result.MessageId = random.Next(1000000, 2000000).ToString();
        result.LocationId = random.Next(1000, 9000).ToString();
        result.StationId = random.Next(1, 30);
        result.TimestampTicks = DateTimeOffset.UtcNow.Ticks;

        var itemCount = random.Next(15, 25);
        for (var loop = 0; loop < itemCount; loop++)
        {
            result.DummyItemList.Add(new DummyRequestItem()
            {
                Property1 = random.Next(1000000, 2000000).ToString(),
                Property2 = random.Next(1000000, 2000000).ToString(),
                Property3 = random.Next(1000000, 2000000),
                Property4 = random.Next(1000000, 2000000),
                Property5 = random.Next(1000000, 2000000).ToString(),
                Property6 = random.Next(1000000, 2000000)
            });
        }

        return result;
    }
}

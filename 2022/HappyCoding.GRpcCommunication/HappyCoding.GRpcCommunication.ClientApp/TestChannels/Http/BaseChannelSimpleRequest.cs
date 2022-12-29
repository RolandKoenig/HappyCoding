using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using HappyCoding.GRpcCommunication.Shared.Dtos;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;

internal abstract class BaseChannelSimpleRequest : BaseChannel
{
    private HttpClient? _httpClient;
    private bool _lastGetSuccessful;

    /// <inheritdoc />
    public override bool IsConnected
    {
        get
        {
            if (_httpClient == null) { return false; }
            return _lastGetSuccessful;
        }
    }

    protected abstract bool IsParallelChannel { get; }

    /// <inheritdoc />
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var options = await ClientOptions.LoadAsync(cancellationToken);

        _httpClient = this.CreateClient(options);

        if (this.IsParallelChannel)
        {
            for (var loop = 0; loop < options.CountParallelLoopsOnParallelChannels; loop++)
            {
                this.Run(loop, _httpClient, options.SpikeThresholdMS, options.DelayBetweenCallsMS);
            }
        }
        else
        {
            this.Run(0, _httpClient, options.SpikeThresholdMS, options.DelayBetweenCallsMS);
        }
    }

    protected abstract HttpClient CreateClient(ClientOptions options);

    /// <inheritdoc />
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _httpClient = null;

        return Task.CompletedTask;
    }

    private async void Run(int threadId, HttpClient client, uint spikeThresholdMS, ushort delayBetweenCallsMS)
    {
        await Task.Delay(100)
            .ConfigureAwait(false);

        while (client == _httpClient)
        {
            try
            {
                var requestObj = new SimpleRequestDto() { Name = "Test" };

                var stopWatch = Stopwatch.StartNew();

                var response = await client.PostAsJsonAsync(
                    "/http/SimpleRequest",
                    requestObj);
                response.EnsureSuccessStatusCode();
                var responseObj = response.Content.ReadFromJsonAsync<SimpleResponseDto>();

                var totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
                base.NotifySuccess(threadId, totalMilliseconds, totalMilliseconds > spikeThresholdMS);

                _lastGetSuccessful = true;
            }
            catch (TaskCanceledException)
            {
                if (client == _httpClient)
                {
                    NotifyTimeout();
                    _lastGetSuccessful = false;
                }
            }
            catch (Exception ex)
            {
                if (client == _httpClient)
                {
                    NotifyError(ex.ToString());
                    _lastGetSuccessful = false;
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

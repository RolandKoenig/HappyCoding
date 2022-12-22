using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

internal class Http2Channel : BaseChannel
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

    /// <inheritdoc />
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var options = await ClientOptions.LoadAsync(cancellationToken);

        var protocol = options.UseHttps ? "https" : "http";

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri($"{protocol}://{options.TargetHost}:{options.Port}");
        _httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        _httpClient.DefaultRequestVersion = new Version(2, 0);
        _httpClient.Timeout = TimeSpan.FromMilliseconds(options.CallTimeoutMS);

        Run(_httpClient, options.DelayBetweenCallsMS);
    }

    /// <inheritdoc />
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _httpClient = null;

        return Task.CompletedTask;
    }

    private async void Run(HttpClient client, ushort delayBetweenCallsMS)
    {
        await Task.Delay(delayBetweenCallsMS)
            .ConfigureAwait(false);

        while (client == _httpClient)
        {
            try
            {
                var stopWatch = Stopwatch.StartNew();

                var response = await client.GetAsync("/");
                response.EnsureSuccessStatusCode();

                base.NotifySuccess(stopWatch.Elapsed.TotalMilliseconds);

                _lastGetSuccessful = true;
            }
            catch (TaskCanceledException)
            {
                if (client == _httpClient)
                {
                    base.NotifyTimeout();
                    _lastGetSuccessful = false;
                }
            }
            catch (Exception ex)
            {
                if (client == _httpClient)
                {
                    base.NotifyError(ex.ToString());
                    _lastGetSuccessful = false;
                }
            }

            await Task.Delay(delayBetweenCallsMS)
                .ConfigureAwait(false);
        }
    }
}

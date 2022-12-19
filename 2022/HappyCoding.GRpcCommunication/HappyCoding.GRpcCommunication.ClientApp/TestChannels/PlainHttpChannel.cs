using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

internal class PlainHttpChannel : ITestChannel
{
    private HttpClient? _httpClient;
    private bool _lastGetSuccessful;
    private ulong _countSuccess;
    private ulong _countErrors;
    private string _lastErrorDetails = string.Empty;

    /// <inheritdoc />
    public bool IsConnected
    {
        get
        {
            if (_httpClient == null) { return false; }
            return _lastGetSuccessful;
        }
    }

    /// <inheritdoc />
    public ulong CountSuccess => _countSuccess;

    /// <inheritdoc />
    public ulong CountErrors => _countErrors;

    /// <inheritdoc />
    public string LastErrorDetails => _lastErrorDetails;

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
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
    public Task StopAsync(CancellationToken cancellationToken)
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
                var response = await client.GetAsync("/");
                response.EnsureSuccessStatusCode();

                Interlocked.Increment(ref _countSuccess);
                _lastGetSuccessful = true;
            }
            catch (Exception ex)
            {
                if (client == _httpClient)
                {
                    Interlocked.Increment(ref _countErrors);
                    _lastErrorDetails = ex.ToString();
                    _lastGetSuccessful = false;
                }
            }

            await Task.Delay(delayBetweenCallsMS)
                .ConfigureAwait(false);
        }
    }
}

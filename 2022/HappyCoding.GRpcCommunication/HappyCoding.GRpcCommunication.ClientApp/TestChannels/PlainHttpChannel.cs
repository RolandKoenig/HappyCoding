using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

internal class PlainHttpChannel : ITestChannel
{
    private HttpClient? _httpClient;
    private bool _lastGetSuccessful;

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
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var options = await ClientOptions.LoadAsync(cancellationToken);

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri($"http://{options.TargetHost}:{options.Port}");
        _httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        _httpClient.DefaultRequestVersion = new Version(2, 0);

        Run(_httpClient);
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _httpClient = null;

        return Task.CompletedTask;
    }

    private async void Run(HttpClient client)
    {
        while (client == _httpClient)
        {
            await Task.Delay(100)
                .ConfigureAwait(false);

            try
            {
                var response = await client.GetAsync("/");
                response.EnsureSuccessStatusCode();

                _lastGetSuccessful = true;
            }
            catch (Exception)
            {
                _lastGetSuccessful = false;
            }
        }
    }
}

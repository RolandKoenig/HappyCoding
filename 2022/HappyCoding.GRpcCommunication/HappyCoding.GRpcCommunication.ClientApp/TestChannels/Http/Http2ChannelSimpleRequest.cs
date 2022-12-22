using System;
using System.Net.Http;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;

internal class Http2ChannelSimpleRequest : BaseChannelSimpleRequest
{
    /// <inheritdoc />
    protected override bool IsParallelChannel => false;

    protected override HttpClient CreateClient(ClientOptions options)
    {
        var protocol = options.UseHttps ? "https" : "http";

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri($"{protocol}://{options.TargetHost}:{options.PortHttp2}");
        httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        httpClient.DefaultRequestVersion = new Version(2, 0);
        httpClient.Timeout = TimeSpan.FromMilliseconds(options.CallTimeoutMS);

        return httpClient;
    }
}

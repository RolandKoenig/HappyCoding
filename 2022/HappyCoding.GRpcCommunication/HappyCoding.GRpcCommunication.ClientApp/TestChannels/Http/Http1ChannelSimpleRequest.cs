using System;
using System.Net.Http;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;

internal class Http1ChannelSimpleRequest : BaseChannelSimpleRequest
{
    /// <inheritdoc />
    protected override bool IsParallelChannel => false;

    /// <inheritdoc />
    protected override HttpClient CreateClient(ClientOptions options)
    {
        var protocol = options.UseHttps ? "https" : "http";

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri($"{protocol}://{options.TargetHost}:{options.PortHttp1}");
        httpClient.Timeout = TimeSpan.FromMilliseconds(options.CallTimeoutMS);
        return httpClient;
    }
}

using System;
using System.Net;
using System.Net.Http;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;

internal class Http2ChannelComplexRequestParallel : BaseChannelComplexRequest
{
    /// <inheritdoc />
    protected override bool IsParallelChannel => true;

    protected override HttpClient CreateClient(ClientOptions options)
    {
        return HttpHelper.BuildHttpClient(options, HttpVersion.Version20, options.PortHttp2);
    }
}

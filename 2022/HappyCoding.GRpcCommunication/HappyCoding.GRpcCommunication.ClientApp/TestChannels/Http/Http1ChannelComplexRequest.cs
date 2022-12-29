using System.Net;
using System.Net.Http;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;

internal class Http1ChannelComplexRequest : BaseChannelComplexRequest
{
    /// <inheritdoc />
    protected override bool IsParallelChannel => false;

    /// <inheritdoc />
    protected override HttpClient CreateClient(ClientOptions options)
    {
        return HttpHelper.BuildHttpClient(options, HttpVersion.Version11, options.PortHttp1);
    }
}

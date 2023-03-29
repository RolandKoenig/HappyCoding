namespace HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

public class LoadBalancingTargetHost
{
    public string Host { get; set; } = string.Empty;

    public ushort Port { get; set; } = 0;

    public LoadBalancingTargetHost()
    {

    }

    public LoadBalancingTargetHost(string host, ushort port)
    {
        this.Host = host;
        this.Port = port;
    }
}

namespace HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

public record LoadBalancingTargetHost
{
    public string Host { get; set; } = string.Empty;

    public ushort Port { get; set; } = 0;
}

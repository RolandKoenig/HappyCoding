namespace HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

public class LoadBalancingGroup
{
    public string Name { get; set; } = string.Empty;

    public List<LoadBalancingTargetHost> TargetHosts { get; set; } = new();
}

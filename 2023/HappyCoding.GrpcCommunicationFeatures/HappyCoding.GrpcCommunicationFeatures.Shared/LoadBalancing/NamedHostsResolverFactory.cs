using Grpc.Net.Client.Balancer;

namespace HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

internal class NamedHostsResolverFactory : ResolverFactory
{
    private readonly IReadOnlyList<LoadBalancingGroup> _groups;

    /// <inheritdoc />
    public override string Name { get; }

    public NamedHostsResolverFactory(string scheme, IReadOnlyList<LoadBalancingGroup> groups)
    {
        this.Name = scheme;
        _groups = groups;
    }

    /// <inheritdoc />
    public override Resolver Create(ResolverOptions options)
    {
        foreach (var actGroup in _groups)
        {
            if (actGroup.Name.Equals(options.Address.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return new FixedHostsResolver(actGroup.TargetHosts);
            }
        }

        return new FixedHostsResolver(Array.Empty<LoadBalancingTargetHost>());
    }

    private class FixedHostsResolver : Resolver
    {
        private readonly IReadOnlyList<LoadBalancingTargetHost> _targetAddresses;

        public FixedHostsResolver(IReadOnlyList<LoadBalancingTargetHost> targetAddresses)
        {
            _targetAddresses = targetAddresses;
        }

        /// <inheritdoc />
        public override void Start(Action<ResolverResult> listener)
        {
            
            listener(ResolverResult.ForResult(_targetAddresses
                .Select(x => new BalancerAddress(x.Host, x.Port))
                .ToArray()));
        }
    }
}

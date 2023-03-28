using Grpc.Net.Client.Balancer;

namespace HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

internal class FixedHostsResolverFactory : ResolverFactory
{
    private readonly LoadBalancingTargetHost[] _addresses;

    /// <inheritdoc />
    public override string Name { get; }

    public FixedHostsResolverFactory(string scheme, LoadBalancingTargetHost[] addresses)
    {
        this.Name = scheme;
        _addresses = addresses;
    }

    /// <inheritdoc />
    public override Resolver Create(ResolverOptions options)
    {
        return new FixedHostsResolver(this);
    }

    private class FixedHostsResolver : Resolver
    {
        private readonly FixedHostsResolverFactory _owner;

        public FixedHostsResolver(FixedHostsResolverFactory owner)
        {
            _owner = owner;
        }

        /// <inheritdoc />
        public override void Start(Action<ResolverResult> listener)
        {
            listener(ResolverResult.ForResult(_owner._addresses
                .Select(x => new BalancerAddress(x.Host, x.Port))
                .ToArray()));
        }
    }
}

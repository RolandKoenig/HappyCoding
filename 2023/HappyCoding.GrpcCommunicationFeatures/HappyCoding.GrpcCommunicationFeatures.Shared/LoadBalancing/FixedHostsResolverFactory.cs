using Grpc.Net.Client.Balancer;

namespace HappyCoding.GrpcCommunicationFeatures.Shared.LoadBalancing;

internal class FixedHostsResolverFactory : ResolverFactory
{
    private readonly string _scheme;
    private readonly BalancerAddress[] _addresses;

    /// <inheritdoc />
    public override string Name => _scheme;

    public FixedHostsResolverFactory(string scheme, BalancerAddress[] addresses)
    {
        _scheme = scheme;
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
            listener(ResolverResult.ForResult(_owner._addresses));
        }
    }
}

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HappyCoding.AspNetCoreHealthChecks.HealthChecks;

internal class AlwaysSuccessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}
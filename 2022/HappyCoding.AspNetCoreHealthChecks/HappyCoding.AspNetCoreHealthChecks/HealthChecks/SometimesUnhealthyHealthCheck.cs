using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HappyCoding.AspNetCoreHealthChecks.HealthChecks;

internal class SometimesUnhealthyHealthCheck : IHealthCheck
{
    private readonly Random _random;

    public SometimesUnhealthyHealthCheck()
    {
        _random = new Random(Environment.TickCount);
    }
    
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        if (_random.NextDouble() < 0.5)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Healthy by random value"));
        }
        else
        {
            
            return Task.FromResult(new HealthCheckResult(
                context.Registration.FailureStatus,
                "Unhealthy by random value"));
        }
    }
}
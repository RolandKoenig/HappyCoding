using Hangfire;
using HappyCoding.AspNetCoreWithHangfire.Application;
using Microsoft.Extensions.Hosting;

namespace HappyCoding.AspNetCoreWithHangfire.Jobs;

internal class BackgroundJobBootstrap : BackgroundService
{
    private readonly IRecurringJobManager _recurringJobManager;

    public BackgroundJobBootstrap(IRecurringJobManager recurringJobManager)
    {
        _recurringJobManager = recurringJobManager;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _recurringJobManager.AddOrUpdate(
            nameof(JobExecutor.ExecuteDummyRecuringJob),
            () => JobExecutor.ExecuteDummyRecuringJob(),
            Cron.Minutely);

        return Task.CompletedTask;
    }
}

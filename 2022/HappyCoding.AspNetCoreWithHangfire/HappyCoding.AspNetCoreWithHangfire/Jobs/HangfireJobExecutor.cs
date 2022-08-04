namespace HappyCoding.AspNetCoreWithHangfire.Jobs;

public static class HangfireJobExecutor
{
    public static async Task ExecuteJobAsync<TRequest>(TRequest request)
    {
        await Task.Delay(1000);
    }
}

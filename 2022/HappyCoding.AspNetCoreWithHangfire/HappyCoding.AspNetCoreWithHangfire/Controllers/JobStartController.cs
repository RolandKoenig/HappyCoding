using Hangfire;
using HappyCoding.AspNetCoreWithHangfire.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace HappyCoding.AspNetCoreWithHangfire.Controllers;
[ApiController]
[Route("[controller]")]
public class JobStartController : ControllerBase
{
    private readonly ILogger<JobStartController> _logger;

    public JobStartController(ILogger<JobStartController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult StartJob(int delayTimeMS)
    {
        BackgroundJob.Schedule(
            () => HangfireJobExecutor.ExecuteJobAsync("Test string"),
            TimeSpan.FromMilliseconds(delayTimeMS));

        return Ok();
    }
}

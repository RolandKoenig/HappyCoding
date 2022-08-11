using Hangfire;
using HappyCoding.AspNetCoreWithHangfire.Application;
using Microsoft.AspNetCore.Mvc;

namespace HappyCoding.AspNetCoreWithHangfire.Controllers;

[ApiController]
[Route("[controller]")]
public class JobStartController : ControllerBase
{
    private readonly ILogger<JobStartController> _logger;
    private readonly IBackgroundJobClient _jobClient;

    public JobStartController(
        ILogger<JobStartController> logger,
        IBackgroundJobClient jobClient)
    {
        _logger = logger;
        _jobClient = jobClient;
    }

    [HttpPost(nameof(StartJob1))]
    public IActionResult StartJob1(int delayTimeMS)
    {
        _jobClient.Schedule(
            () => JobExecutor.ExecuteDummyJob1(),
            TimeSpan.FromMilliseconds(delayTimeMS));

        return Ok();
    }

    [HttpPost(nameof(StartJob2))]
    public IActionResult StartJob2(int delayTimeMS)
    {
        _jobClient.Schedule(
            () => JobExecutor.ExecuteDummyJob2(),
            TimeSpan.FromMilliseconds(delayTimeMS));

        return Ok();
    }
}

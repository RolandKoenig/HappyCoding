using Microsoft.AspNetCore.Mvc;

namespace Service.Api.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetAsync()
        {
            return Task.FromResult<IActionResult>(
                Ok("All OK"));
        }
    }
}

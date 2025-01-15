using HappyCoding.HexagonalArchitecture.Application;
using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HappyCoding.HexagonalArchitecture.WebUI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkshopsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public WorkshopsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateWorkshop(
        WorkshopWithoutIDDto workshop,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        return Ok(await _mediator.Send(
            new CreateWorkshopRequest(workshop)));
    }

    [HttpPut]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateWorkshop(
        WorkshopDto workshop,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        return Ok(await _mediator.Send(
            new UpdateWorkshopRequest(workshop)));
    }

    [HttpDelete]
    [Route("{workshopID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteWorkshop(
        Guid workshopID,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        await _mediator.Send(
            new DeleteWorkshopRequest(workshopID));
        
        return Ok();
    }

    [HttpGet("Search")]
    [ProducesResponseType(typeof(IEnumerable<WorkshopShortInfoDto>), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> SearchWorkshops(
        [FromQuery] string? query,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        return Ok(await _mediator.Send(
            new SearchWorkshopsRequest()
            {
                QueryString = query
            }));
    }

    [HttpGet]
    [Route("{workshopID}")]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> GetWorkshop(
        Guid workshopID,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        return Ok(await _mediator.Send(
            new GetWorkshopRequest(workshopID)));
    }
}
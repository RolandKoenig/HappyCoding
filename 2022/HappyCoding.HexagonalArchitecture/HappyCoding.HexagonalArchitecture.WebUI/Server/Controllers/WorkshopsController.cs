using HappyCoding.HexagonalArchitecture.Application;
using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateWorkshop(WorkshopWithoutIDDto workshop)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        return Ok(await _mediator.Send(
            new CreateWorkshopRequest(workshop)));
    }

    [HttpPut]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateWorkshop(WorkshopDto workshop)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        return Ok(await _mediator.Send(
            new UpdateWorkshopRequest(workshop)));
    }

    [HttpDelete]
    [Route("{workshopID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteWorkshop(Guid workshopID)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        await _mediator.Send(
            new DeleteWorkshopRequest(workshopID));
        
        return Ok();
    }

    [HttpGet("Search")]
    [ProducesResponseType(typeof(IEnumerable<WorkshopShortInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchWorkshops([FromQuery] string? query)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        return Ok(await _mediator.Send(
            new SearchWorkshopsRequest()
            {
                QueryString = query
            }));
    }
    
    [HttpGet]
    [Route("{workshopID}")]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkshop(Guid workshopID)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        return Ok(await _mediator.Send(
            new GetWorkshopRequest(workshopID)));
    }
}
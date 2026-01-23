using HappyCoding.HexagonalArchitecture.Application;
using HappyCoding.HexagonalArchitecture.WebUI.Dtos;
using HappyCoding.HexagonalArchitecture.WebUI.Server.Mapper;
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

        var createdWorkshop = await _mediator.Send(
            new CreateWorkshopCommand(
                workshop.Project,
                workshop.Title,
                workshop.StartTimestamp,
                workshop.Protocol.Select(WorkshopMapper.ProtocolEntryDtoToModel)));
        
        return Ok(createdWorkshop.WorkshopToDto());
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

        var updatedWorkshop = await _mediator.Send(
            new UpdateWorkshopCommand(
                workshop.ID,
                workshop.Project,
                workshop.Title,
                workshop.StartTimestamp,
                workshop.Protocol.Select(WorkshopMapper.ProtocolEntryDtoToModel)));
        
        return Ok(updatedWorkshop.WorkshopToDto());
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
            new DeleteWorkshopCommand(workshopID));
        
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

        var queryResult = await _mediator.Send(
            new SearchWorkshopsQuery()
            {
                QueryString = query
            });
        
        return Ok(queryResult.Select(WorkshopMapper.WorkshopShortInfoDto));
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
            new GetWorkshopQuery(workshopID)));
    }
}
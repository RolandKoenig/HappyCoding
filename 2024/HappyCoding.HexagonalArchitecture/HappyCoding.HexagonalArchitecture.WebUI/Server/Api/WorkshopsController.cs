using HappyCoding.HexagonalArchitecture.Application.UseCases;
using HappyCoding.HexagonalArchitecture.WebUI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HappyCoding.HexagonalArchitecture.WebUI.Server.Api;

[ApiController]
[Route("[controller]")]
public class WorkshopsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateWorkshop(
        [FromServices] CreateWorkshopCommandHandler createWorkshopCommandHandler,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions,
        WorkshopWithoutIDDto workshop,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var createdWorkshop = await createWorkshopCommandHandler.HandleAsync(
            new CreateWorkshopCommand(
                workshop.Project,
                workshop.Title,
                workshop.StartTimestamp,
                workshop.Protocol.Select(WorkshopMapper.ProtocolEntryDtoToModel)),
            cancellationToken);
        
        return Ok(createdWorkshop.WorkshopToDto());
    }

    [HttpPut]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateWorkshop(
        [FromServices] UpdateWorkshopCommandHandler updateWorkshopCommandHandler,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions,
        WorkshopDto workshop,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var updatedWorkshop = await updateWorkshopCommandHandler.HandleAsync(
            new UpdateWorkshopCommand(
                workshop.ID,
                workshop.Project,
                workshop.Title,
                workshop.StartTimestamp,
                workshop.Protocol.Select(WorkshopMapper.ProtocolEntryDtoToModel)),
            cancellationToken);
        
        return Ok(updatedWorkshop.WorkshopToDto());
    }

    [HttpDelete]
    [Route("{workshopID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteWorkshop(
        [FromServices] DeleteWorkshopCommandHandler deleteWorkshopCommandHandler,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions,
        Guid workshopID,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        await deleteWorkshopCommandHandler.HandleAsync(
            new DeleteWorkshopCommand(workshopID),
            cancellationToken);
        
        return Ok();
    }

    [HttpGet("Search")]
    [ProducesResponseType(typeof(IEnumerable<WorkshopShortInfoDto>), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> SearchWorkshops(
        [FromServices] SearchWorkshopsQueryHandler searchWorkshopsQueryHandler,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions,
        [FromQuery] string? query,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var queryResult = await searchWorkshopsQueryHandler.HandleAsync(
            new SearchWorkshopsQuery()
            {
                QueryString = query
            },
            cancellationToken);
        
        return Ok(queryResult.Select(WorkshopMapper.WorkshopShortInfoDto));
    }

    [HttpGet]
    [Route("{workshopID}")]
    [ProducesResponseType(typeof(WorkshopDto), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> GetWorkshop(
        [FromServices] GetWorkshopQueryHandler getWorkshopQueryHandler,
        [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions,
        Guid workshopID,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var resultModel = await getWorkshopQueryHandler.HandleAsync(
            new GetWorkshopQuery(workshopID),
            cancellationToken);
        
        return Ok(resultModel.WorkshopToDto());
    }
}
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
    public async Task<IActionResult> CreateWorkshop(WorkshopWithoutIDDto workshop)
    {
        return Ok(await _mediator.Send(new CreateWorkshopRequest()
        {
            Workshop = workshop
        }));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWorkshop(WorkshopDto workshop)
    {
        return Ok(await _mediator.Send(new UpdateWorkshopRequest()
        {
            Workshop = workshop
        }));
    }

    [HttpDelete]
    [Route("{workshopID}")]
    public async Task<IActionResult> DeleteWorkshop(Guid workshopID)
    {
        await _mediator.Send(new DeleteWorkshopRequest()
        {
            WorkshopID = workshopID
        });
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> SearchWorkshops([FromQuery] string? query)
    {
        return Ok(await _mediator.Send(new SearchWorkshopsRequest()
        {
            QueryString = query ?? ""
        }));
    }
    
    [HttpGet]
    [Route("{workshopID}")]
    public async Task<IActionResult> GetWorkshop(Guid workshopID)
    {
        return Ok(await _mediator.Send(new GetWorkshopRequest()
        {
            ID = workshopID
        }));
    }
}
using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HappyCoding.HexagonalArchitecture.WebUI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkshopController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public WorkshopController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(WorkshopDto), 200)]
    public async Task<IActionResult> CreateWorkshop(WorkshopDto workshop)
    {
        return Ok(await _mediator.Send(new CreateWorkshopRequest()
        {
            Workshop = workshop
        }));
    }
}
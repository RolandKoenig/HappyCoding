using HappyCoding.CQSWithMediatR.Application.Persons.Commands;
using HappyCoding.CQSWithMediatR.Application.Persons.Dtos;
using HappyCoding.CQSWithMediatR.Application.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HappyCoding.CQSWithMediatR.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<PersonDto>> GetPersons()
        => await _mediator.Send(new SearchPersonsQuery());

    [HttpPost]
    public async Task AddPerson([FromBody] PersonDto person)
        => await _mediator.Send(new AddPersonCommand(person));
}
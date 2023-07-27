using HappyCoding.TestingWithContainers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.TestingWithContainers.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DummyDataController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PersonDataRow), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] PersonDataDbContext dbContext)
    {
        var allPersons = await dbContext.Persons.ToArrayAsync();

        return allPersons.Length > 0 ? Ok(allPersons) : NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(PersonDataRow), StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> CreatePerson(
        PersonDataRow person,
        [FromServices] PersonDataDbContext dbContext)
    {
        var addedPerson = await dbContext.Persons.AddAsync(person);
        await dbContext.SaveChangesAsync();

        return Created(addedPerson.Entity.Id.ToString(), addedPerson.Entity);
    }

    [HttpPut]
    [ProducesResponseType(typeof(PersonDataRow), StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdatePerson(
        PersonDataRow person,
        [FromServices] PersonDataDbContext dbContext)
    {
        var existingPerson = await dbContext.Persons
            .Where(x => x.Id == person.Id)
            .FirstOrDefaultAsync();
        if (existingPerson == null) { return NotFound(); }

        dbContext.Persons.Entry(existingPerson).CurrentValues.SetValues(person);

        await dbContext.SaveChangesAsync();

        return Ok(existingPerson);
    }
}

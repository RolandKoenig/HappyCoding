using Microsoft.EntityFrameworkCore;

namespace HappyCoding.TestingWithContainers.Data;

public class PersonDataDbContext : DbContext
{
    public DbSet<PersonDataRow> Persons { get; set; } = null!;

    public PersonDataDbContext(DbContextOptions<PersonDataDbContext> options)
        : base(options)
    {

    }
}

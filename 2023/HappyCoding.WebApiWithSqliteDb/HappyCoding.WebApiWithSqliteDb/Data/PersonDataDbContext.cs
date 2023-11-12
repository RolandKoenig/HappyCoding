using Microsoft.EntityFrameworkCore;

namespace HappyCoding.WebApiWithSqliteDb.Data;

public class PersonDataDbContext : DbContext
{
    public DbSet<PersonDataRow> Persons { get; set; } = null!;

    public PersonDataDbContext(DbContextOptions<PersonDataDbContext> options)
        : base(options)
    {

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.WebApiWithSqliteDb.Data;

public class PersonDataDbContextFactory : IDesignTimeDbContextFactory<PersonDataDbContext>
{
    public PersonDataDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<PersonDataDbContext>();
        optionsBuilder.UseSqlite();
        return new PersonDataDbContext(optionsBuilder.Options);
    }
}

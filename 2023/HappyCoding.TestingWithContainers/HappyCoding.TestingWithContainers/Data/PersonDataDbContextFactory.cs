using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.TestingWithContainers.Data;

public class PersonDataDbContextFactory : IDesignTimeDbContextFactory<PersonDataDbContext>
{
    public PersonDataDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<PersonDataDbContext>();
        optionsBuilder.UseSqlServer();
        return new PersonDataDbContext(optionsBuilder.Options);
    }
}

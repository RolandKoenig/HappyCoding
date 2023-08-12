using HappyCoding.EFCoreOwnedTypes.Data;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreOwnedTypes;

public class MyDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;

    public DbSet<Company> Campanies { get; set; } = null!;

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {

    }
}

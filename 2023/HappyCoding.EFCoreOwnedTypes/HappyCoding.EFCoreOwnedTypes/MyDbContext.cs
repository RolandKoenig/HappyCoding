using HappyCoding.EFCoreOwnedTypes.Data;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreOwnedTypes;

public class MyDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;

    public DbSet<Company> Companies { get; set; } = null!;

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {

    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var tableCompany = modelBuilder.Entity<Company>().ToTable("Companies");
        tableCompany.OwnsOne<Address>(x => x.Address);
        tableCompany.OwnsOne<Address>(x => x.SecondaryAddress);
        tableCompany.OwnsMany<Address>(x => x.AdditionalAddresses);

        var tablePerson = modelBuilder.Entity<Person>().ToTable("Persons");
        tablePerson.OwnsOne<Address>(x => x.Address);
    }
}

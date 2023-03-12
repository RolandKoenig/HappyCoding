using Ardalis.SmartEnum.EFCore;
using HappyCoding.SmartEnumerations.Data;
using HappyCoding.SmartEnumerations.Enums;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.SmartEnumerations.SqlServer;

public class TestingDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; } = null!;

    public TestingDbContext(DbContextOptions<TestingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var tableAddresses = modelBuilder.Entity<Address>().ToTable("Addresses");
        tableAddresses.Property(x => x.Country)
            .HasConversion(new SmartEnumConverter<Nation, string>())
            .HasMaxLength(2);
    }
}

using HappyCoding.EFCoreJsonModelValueConverter.Model;
using HappyCoding.EFCoreJsonModelValueConverter.Util;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreJsonModelValueConverter;

public class TestingDBContext : DbContext
{
    public DbSet<TestingDataRow> Testing { get; set; } = null!;

    public TestingDBContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var testingTable = modelBuilder.Entity<TestingDataRow>().ToTable("Testing");
        testingTable.HasKey(x => x.Id);
        testingTable
            .Property(x => x.JsonData)
            .HasConversion<byte[]>(new JsonValueConverter<TestingJsonData>());
    }
}

using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context1;

internal class DataContext1 : DbContext
{
    public DbSet<DataRow1> DataRows1 { get; set; } = null!;

    public DataContext1(DbContextOptions<DataContext1> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("ctx1");
    }
}

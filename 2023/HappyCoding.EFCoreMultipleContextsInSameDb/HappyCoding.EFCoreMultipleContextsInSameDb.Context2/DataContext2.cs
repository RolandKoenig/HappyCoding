using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context2;

internal class DataContext2 : DbContext
{
    public DbSet<DataRow2> DataRows2 { get; set; } = null!;

    public DataContext2(DbContextOptions<DataContext2> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("ctx2");
    }
}

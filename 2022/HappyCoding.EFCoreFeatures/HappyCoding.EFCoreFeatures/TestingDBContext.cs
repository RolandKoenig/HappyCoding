using HappyCoding.EFCoreFeatures.Data;
using HappyCoding.EFCoreFeatures.Util;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreFeatures;

internal class TestingDBContext : DbContext
{
    public DbSet<TestingRow> Testing { get; set; } = null!;

    public DbSet<ParentRow> Parents { get; set; } = null!;

    public DbSet<ChildRow> Childs { get; set; } = null!;

    public TestingDBContext(DbContextOptions options)
        : base(options)
    {

    }

    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<JsonModel<TestingTagCollection>>()
            .HaveConversion<JsonModelConverter<TestingTagCollection>, JsonModelComparer<TestingTagCollection>>();
    }
}

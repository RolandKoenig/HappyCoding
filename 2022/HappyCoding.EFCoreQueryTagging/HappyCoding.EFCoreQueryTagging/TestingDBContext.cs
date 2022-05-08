using HappyCoding.EFCoreQueryTagging.Model;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFCoreQueryTagging;

public class TestingDBContext : DbContext
{
    public DbSet<Procedure> Processes { get; set; } = null!;

    public DbSet<ProcedureActivity> ProcessActivities { get; set; } = null!;

    public TestingDBContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var processTable = modelBuilder.Entity<Procedure>().ToTable("Procedure");
        processTable.HasKey(x => x.ID);
        processTable
            .Property(x => x.ID)
            .HasMaxLength(30)
            .ValueGeneratedNever();
        processTable.Property(x => x.CreateTimestampUtc);
        processTable.Property(x => x.Field1);
        processTable.Property(x => x.Field2);
        processTable.Property(x => x.Field3);
        processTable
            .Property(x => x.Field4)
            .HasMaxLength(10);
        processTable
            .Property(x => x.Field5)
            .HasMaxLength(10);
        processTable.Property(x => x.Field6);

        var processActivityTable = modelBuilder.Entity<ProcedureActivity>().ToTable("ProcedureActivity");
        processActivityTable.HasKey(x => x.ProcessActivityID);
        processActivityTable
            .Property(x => x.ProcessActivityID)
            .ValueGeneratedOnAdd();
        processActivityTable
            .Property(x => x.ProcessID)
            .HasMaxLength(30);
        processActivityTable.Property(x => x.ActivityTimestampUtc);
        processActivityTable.Property(x => x.Field1);
        processActivityTable.Property(x => x.Field2);
        processActivityTable.Property(x => x.Field3);
        processActivityTable
            .Property(x => x.Field4)
            .HasMaxLength(10);
        processActivityTable
            .Property(x => x.Field5)
            .HasMaxLength(10);
        processActivityTable.Property(x => x.Field6);
        processActivityTable.HasIndex(x => x.ProcessID);

        processTable
            .HasMany(x => x.Activities)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.ProcessID);
    }
}
using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.SQLiteAdapter.Converters;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter;

internal class AppDBContext : DbContext
{
    public DbSet<Workshop> Workshops { get; private set; } = null!;
    
    public AppDBContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var tableWorkshops = modelBuilder.Entity<Workshop>().ToTable("Workshop");
        tableWorkshops.HasKey(x => x.ID);
        tableWorkshops
            .Property(x => x.ID)
            .ValueGeneratedNever();
        tableWorkshops
            .Property(x => x.Project)
            .HasMaxLength(50);
        tableWorkshops
            .Property(x => x.Title)
            .HasMaxLength(100);
        tableWorkshops.Property(x => x.StartTimestamp);
        tableWorkshops.Property(x => x.Duration);
        tableWorkshops
            .Property(x => x.Participants)
            .HasConversion(new ParticipantsConverter())
            .HasMaxLength(500);

        var tableProtocolEntries = modelBuilder.Entity<ProtocolEntry>().ToTable("Protocol");
        tableProtocolEntries.HasKey(x => x.ID);
        tableProtocolEntries
            .Property(x => x.ID)
            .ValueGeneratedNever();
        tableProtocolEntries
            .Property(x => x.Text)
            .HasMaxLength(300);
        tableProtocolEntries.Property(x => x.EntryType);
        tableProtocolEntries.Property(x => x.Priority);

        //public Guid ID { get; private set; }
//
        //public string Text { get; private set; }
        //
        //public ProtocolEntryType EntryType { get; private set; }
        //
        //public int Priority { get; private set; }
        //
        //public string Responsible { get; private set; }
        //
        //public DateTimeOffset ChangeDate { get; private set; }
    }
}
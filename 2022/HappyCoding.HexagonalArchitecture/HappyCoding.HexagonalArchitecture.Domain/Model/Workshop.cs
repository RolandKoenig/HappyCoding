using System.Runtime.CompilerServices;

namespace HappyCoding.HexagonalArchitecture.Domain.Model;

public class Workshop
{
    public Guid ID { get; private set; } 

    public string Project { get; private set; } = "";

    public string Title { get; private set; } = "";
    
    public DateTimeOffset StartTimestamp { get; private set; }
    
    public TimeSpan Duration { get; private set; }

    public IReadOnlyList<string> Participants { get; private set; } = Array.Empty<string>();

    public IReadOnlyList<ProtocolEntry> Protocol { get; private set; } = Array.Empty<ProtocolEntry>();

    // Constructor is needed for factory methods and EntityFrameworkCore
#pragma warning disable CS8618
    private Workshop()
    {
        
    }
#pragma warning restore CS8618

    public static Workshop CreateNew(
        string project, string title, DateTimeOffset startTimestamp, TimeSpan duration,
        IEnumerable<string> participants,
        IEnumerable<ProtocolEntry> protocol)
    {
        return new Workshop()
        {
            ID = Guid.NewGuid(),
            Project = project,
            Title = title,
            StartTimestamp  = startTimestamp,
            Duration = duration,
            Participants = participants.ToArray(),
            Protocol = protocol.ToArray()
        };
    }

    public void Update(
        string project, string title, DateTimeOffset startTimestamp, TimeSpan duration,
        IEnumerable<string> participants,
        IEnumerable<ProtocolEntry> protocol)
    {
        this.Project = project;
        this.Title = title;
        this.StartTimestamp = startTimestamp;
        this.Duration = duration;
        this.Participants = participants.ToArray();
        this.Protocol = protocol.ToArray();
    }
}
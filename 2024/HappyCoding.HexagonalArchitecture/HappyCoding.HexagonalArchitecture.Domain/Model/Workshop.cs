namespace HappyCoding.HexagonalArchitecture.Domain.Model;

public class Workshop
{
    private List<ProtocolEntry> _protocol = new List<ProtocolEntry>();

    public Guid ID { get; private set; } 

    public string Project { get; private set; } = "";

    public string Title { get; private set; } = "";
    
    public DateTimeOffset StartTimestamp { get; private set; }

    public IReadOnlyList<ProtocolEntry> Protocol => _protocol;

    // Constructor is needed for factory methods and EntityFrameworkCore
#pragma warning disable CS8618
    private Workshop()
    {
        
    }
#pragma warning restore CS8618

    public static Workshop CreateNew(
        string project, string title, DateTimeOffset startTimestamp, IEnumerable<ProtocolEntry> protocol)
    {
        return new Workshop()
        {
            ID = Guid.NewGuid(),
            Project = project,
            Title = title,
            StartTimestamp  = startTimestamp,
            _protocol = protocol.ToList()
        };
    }

    public void Update(
        string project, string title, DateTimeOffset startTimestamp, IEnumerable<ProtocolEntry> protocol)
    {
        this.Project = project;
        this.Title = title;
        this.StartTimestamp = startTimestamp;
        _protocol = protocol.ToList();
    }
}
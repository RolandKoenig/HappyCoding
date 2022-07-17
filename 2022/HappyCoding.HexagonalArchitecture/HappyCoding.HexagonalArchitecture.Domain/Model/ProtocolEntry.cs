namespace HappyCoding.HexagonalArchitecture.Domain.Model;

public class ProtocolEntry
{
    public Guid ID { get; private set; }

    public string Text { get; private set; }
    
    public ProtocolEntryType EntryType { get; private set; }
    
    public ProtocolEntryPriority Priority { get; private set; }
    
    public string Responsible { get; private set; }
    
    public DateTimeOffset ChangeDate { get; private set; }

    // Constructor is needed for factory methods and EntityFrameworkCore
#pragma warning disable CS8618
    private ProtocolEntry()
    {
        
    }
#pragma warning restore CS8618

    public static ProtocolEntry CreateNew(
        string text, ProtocolEntryType entryType, ProtocolEntryPriority priority,
        string responsible, DateTimeOffset createDate)
    {
        return new ProtocolEntry()
        {
            ID = Guid.NewGuid(),
            Text = text,
            EntryType = entryType,
            Priority = priority,
            Responsible = responsible,
            ChangeDate = createDate
        };
    }
}
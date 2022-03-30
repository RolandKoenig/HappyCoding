namespace HappyCoding.EFCoreFeatures.Data;

public class NewestChildWithParent
{
    public string Name { get; private set; }

    public DateTimeOffset Timestamp { get; private set; }

    public NewestChildWithParent(string name, DateTimeOffset timestamp)
    {
        this.Name = name;
        this.Timestamp = timestamp;
    }
}

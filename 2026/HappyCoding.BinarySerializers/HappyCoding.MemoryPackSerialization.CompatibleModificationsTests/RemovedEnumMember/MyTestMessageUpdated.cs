using MemoryPack;

namespace HappyCoding.MemoryPackSerialization.CompatibleModificationsTests.RemovedEnumMember;

[MemoryPackable(GenerateType.VersionTolerant)]
public partial class MyTestMessageUpdated
{
    [MemoryPackOrder(0)]
    public string FirstName { get; set; } = string.Empty;

    [MemoryPackOrder(1)]
    public string LastName { get; set; } = string.Empty;

    [MemoryPackOrder(2)]
    public int Age { get; set; }
    
    [MemoryPackOrder(3)]
    public PreferredWayOfCommunicationUpdated PreferredWayOfCommunication { get; set; }
    
    [MemoryPackOrder(10)]
    public List<string> Emails { get; set; } = new();
}

public enum PreferredWayOfCommunicationUpdated
{
    Unspecified = 0,
    Email = 1,
    // Phone = 2, // Removed
    Post = 3
}

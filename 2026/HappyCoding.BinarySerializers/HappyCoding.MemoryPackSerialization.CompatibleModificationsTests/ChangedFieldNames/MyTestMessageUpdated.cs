using MemoryPack;

namespace HappyCoding.MemoryPackSerialization.CompatibleModificationsTests.ChangedFieldNames;

[MemoryPackable(GenerateType.VersionTolerant)]
public partial class MyTestMessageUpdated
{
    [MemoryPackOrder(0)]
    public string VorName { get; set; } = string.Empty;

    [MemoryPackOrder(1)]
    public string NachName { get; set; } = string.Empty;

    [MemoryPackOrder(2)]
    public int Alter { get; set; }

    [MemoryPackOrder(10)]
    public List<string> Emails { get; set; } = new();
}

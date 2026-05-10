using MemoryPack;

namespace HappyCoding.MemoryPackSerialization.CompatibleModificationsTests.ChangeFieldType;

[MemoryPackable(GenerateType.VersionTolerant)]
public partial class MyTestMessageUpdated
{
    [MemoryPackOrder(0)]
    public string FirstName { get; set; } = string.Empty;

    [MemoryPackOrder(1)]
    public string LastName { get; set; } = string.Empty;

    [MemoryPackOrder(2)]
    public string Age { get; set; } = string.Empty;

    [MemoryPackOrder(10)]
    public List<string> Emails { get; set; } = new();
}

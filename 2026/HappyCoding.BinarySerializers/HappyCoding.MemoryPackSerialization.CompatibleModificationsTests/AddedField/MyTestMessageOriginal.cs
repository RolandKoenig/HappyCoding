using MemoryPack;

namespace HappyCoding.MemoryPackSerialization.CompatibleModificationsTests.AddedField;

[MemoryPackable(GenerateType.VersionTolerant)]
public partial class MyTestMessageOriginal
{
    [MemoryPackOrder(0)]
    public string FirstName { get; set; } = string.Empty;

    [MemoryPackOrder(1)]
    public string LastName { get; set; } = string.Empty;

    [MemoryPackOrder(2)]
    public int Age { get; set; }

    [MemoryPackOrder(10)]
    public List<string> Emails { get; set; } = new();
}

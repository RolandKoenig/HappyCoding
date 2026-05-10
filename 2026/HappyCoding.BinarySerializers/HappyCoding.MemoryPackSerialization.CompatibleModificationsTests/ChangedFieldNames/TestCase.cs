using MemoryPack;
using Xunit;

namespace HappyCoding.MemoryPackSerialization.CompatibleModificationsTests.ChangedFieldNames;

public class TestCase
{
    [Fact]
    public void ExecuteTestCase()
    {
        // Prepare original object
        var original = new MyTestMessageOriginal();
        original.FirstName = "Test FirstName";
        original.LastName = "Test LastName";
        original.Age = 8;
        original.Emails.Add("test@test.com");
        original.Emails.Add("test@test.de");

        // MemoryPack serialization
        var serializedBytes = MemoryPackSerializer.Serialize(original);

        // Deserialize using updated object
        var updated = MemoryPackSerializer.Deserialize<MyTestMessageUpdated>(serializedBytes);

        // Asserts
        Assert.NotNull(updated);
        Assert.Equal(original.FirstName, updated.VorName);
        Assert.Equal(original.LastName, updated.NachName);
        Assert.Equal(original.Age, updated.Alter);
        Assert.Equal(original.Emails, updated.Emails);
    }
}

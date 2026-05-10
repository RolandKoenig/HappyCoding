using MemoryPack;
using Xunit;

namespace HappyCoding.MemoryPackSerialization.CompatibleModificationsTests.RemovedField;

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
        // This is expected to fail or behave unexpectedly if MemoryPack doesn't handle missing tags
        var updated = MemoryPackSerializer.Deserialize<MyTestMessageUpdated>(serializedBytes);

        // Asserts
        Assert.NotNull(updated);
        Assert.Equal(original.FirstName, updated.FirstName);
        Assert.Equal(original.LastName, updated.LastName);
        Assert.Equal(original.Emails, updated.Emails);
    }
}

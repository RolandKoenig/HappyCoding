using MemoryPack;
using Xunit;

namespace HappyCoding.MemoryPackSerialization.CompatibleModificationsTests.RemovedEnumMember;

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
        original.PreferredWayOfCommunication = PreferredWayOfCommunication.Phone;

        // MemoryPack serialization
        var serializedBytes = MemoryPackSerializer.Serialize(original);

        // Deserialize using updated object
        var updated = MemoryPackSerializer.Deserialize<MyTestMessageUpdated>(serializedBytes);

        // Asserts
        Assert.NotNull(updated);
        Assert.Equal(original.FirstName, updated.FirstName);
        Assert.Equal(original.LastName, updated.LastName);
        Assert.Equal(original.Age, updated.Age);
        Assert.Equal(original.Emails, updated.Emails);
        // Deserializing a value that is no longer in the enum
        Assert.Equal(2, (int)updated.PreferredWayOfCommunication);
    }
}

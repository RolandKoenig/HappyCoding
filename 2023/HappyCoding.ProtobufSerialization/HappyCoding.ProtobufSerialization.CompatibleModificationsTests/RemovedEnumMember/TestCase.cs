using Google.Protobuf;

namespace HappyCoding.ProtobufSerialization.CompatibleModificationsTests.RemovedEnumMember;

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
        original.PreferredWayOfCommunication = PreferredWayOfCommunication.Email;

        // Protobuf serialization
        using var memStream = new MemoryStream(original.CalculateSize());
        original.WriteTo(memStream);
        var serializedBytes = memStream.ToArray();

        // Deserialize using updated object
        var updated = new MyTestMessageUpdated();
        updated.MergeFrom(serializedBytes);

        // Asserts
        Assert.Equal(original.FirstName, updated.FirstName);
        Assert.Equal(original.LastName, updated.LastName);
        Assert.Equal(original.Age, updated.Age);
        Assert.Equal(original.Emails, updated.Emails);
        Assert.Equal((int)original.PreferredWayOfCommunication, (int)updated.PreferredWayOfCommunication);
    }
}

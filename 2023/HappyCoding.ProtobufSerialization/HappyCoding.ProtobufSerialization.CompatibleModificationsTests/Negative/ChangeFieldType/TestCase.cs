using Google.Protobuf;

namespace HappyCoding.ProtobufSerialization.CompatibleModificationsTests.Negative.ChangeFieldType;

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

        // Protobuf serialization
        using var memStream = new MemoryStream(original.CalculateSize());
        original.WriteTo(memStream);
        var serializedBytes = memStream.ToArray();

        // Deserialize using updated object
        var updated = new MyTestMessageUpdated();
        updated.MergeFrom(serializedBytes);

        // Breaks here because 'age' was not deserialized because of the changed type int32 -> string
        Assert.Equal(original.Age.ToString(), updated.Age);
    }
}

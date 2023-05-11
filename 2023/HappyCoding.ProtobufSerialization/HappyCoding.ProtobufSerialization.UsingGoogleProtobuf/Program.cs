using System.Text;
using System.Text.Json;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using HappyCoding.ProtobufSerialization.UsingGoogleProtobuf.Data;

namespace HappyCoding.ProtobufSerialization.UsingGoogleProtobuf;

public class Program
{
    public static void Main(string[] args)
    {
        // // MyTestMessage
        // var myMessage = new MyTestMessage();
        // myMessage.FirstName = "Test FirstName";
        // myMessage.LastName = "Test LastName";
        // myMessage.Age = 8;
        // myMessage.Emails.Add("test@test.com");
        // myMessage.Emails.Add("test@test.de");

        // // MyTestMessageWithOneOf
        // var myMessage = new MyTestMessageWithOneOf();
        // myMessage.FirstName = "Test FirstName";
        // myMessage.LastName = "Test LastName";
        // myMessage.Age = 8;
        // myMessage.ContactMailAddress = "test@test.de";
        
        // // MyTestMessageWithChildMessage
        // var myMessage = new MyTestMessageWithChildMessage();
        // myMessage.FirstName = "Test FirstName";
        // myMessage.LastName = "Test LastName";
        // myMessage.Age = 8;
        // myMessage.Emails.Add("test@test.com");
        // myMessage.Emails.Add("test@test.de");
        // myMessage.Address = new MyTestMessageChild()
        // {
        //     City = "Testcity",
        //     PostalCode = "12345",
        //     Street = "Teststreet"
        // };
        
        // MyTestMessage
        var myMessage = new MyTestMessageWithTimestamps();
        myMessage.FirstName = "Test FirstName";
        myMessage.LastName = "Test LastName";
        myMessage.Age = 8;
        myMessage.Emails.Add("test@test.com");
        myMessage.Emails.Add("test@test.de");

        var currentTimestamp = DateTimeOffset.Now;
        myMessage.MyTimestampUtc = Timestamp.FromDateTimeOffset(currentTimestamp.ToUniversalTime());
        myMessage.MyTimestampLocal = Timestamp.FromDateTimeOffset(currentTimestamp);

        // Json Serialization
        var serializedBytesJson = JsonSerializer.SerializeToUtf8Bytes(myMessage, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        var serializedJson = Encoding.UTF8.GetString(serializedBytesJson);
        Console.WriteLine($"Json serialization ({serializedBytesJson.Length} bytes):");
        Console.WriteLine(serializedJson);
        Console.WriteLine();

        // Protobuf serialization
        using var memStream = new MemoryStream(myMessage.CalculateSize());
        myMessage.WriteTo(memStream);

        var serializedBytes = memStream.ToArray();

        Console.WriteLine($"Protobuf serialization ({serializedBytes.Length} bytes): ");
        Console.WriteLine(ToHexString(serializedBytes));
        Console.WriteLine();
    }

    private static string ToHexString(byte[] bytes)
    {
        if (bytes.Length == 0) { return string.Empty; }

        var resultBuilder = new StringBuilder(bytes.Length * 3);
        foreach (var actByte in bytes)
        {
            resultBuilder.Append(actByte.ToString("X").PadLeft(2, '0'));
            resultBuilder.Append(' ');
        }

        return resultBuilder.ToString();
    }
}

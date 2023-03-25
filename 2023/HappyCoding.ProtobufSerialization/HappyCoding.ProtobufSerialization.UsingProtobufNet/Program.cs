using System.Text;
using System.Text.Json;
using HappyCoding.ProtobufSerialization.Data;
using ProtoBuf;

namespace HappyCoding.ProtobufSerialization;

internal class Program
{
    static void Main(string[] args)
    {
        var myMessage = new MyTestMessage();
        myMessage.FirstName = "Test FirstName";
        myMessage.LastName = "Test LastName";
        myMessage.Age = 8;
        myMessage.Emails = new[]
        {
            "test@test.com",
            "test@test.de"
        };

        // Json Serialization
        var serializedBytesJson = JsonSerializer.SerializeToUtf8Bytes(myMessage, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        var serializedJson = Encoding.UTF8.GetString(serializedBytesJson);
        Console.WriteLine($"Json serialization ({serializedBytesJson.Length} bytes):");
        Console.WriteLine(serializedJson);
        Console.WriteLine();

        // Protobuf serialization
        using var memStream = new MemoryStream();
        Serializer.Serialize(memStream, myMessage);

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

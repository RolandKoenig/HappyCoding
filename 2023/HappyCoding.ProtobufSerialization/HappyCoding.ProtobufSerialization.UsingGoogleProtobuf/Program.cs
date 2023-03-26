using System.Text;
using System.Text.Json;
using Google.Protobuf;
using HappyCoding.ProtobufSerialization.UsingGoogleProtobuf.Data;

namespace HappyCoding.ProtobufSerialization.UsingGoogleProtobuf;

public class Program
{
    public static void Main(string[] args)
    {
        var myMessage = new MyTestMessage();
        myMessage.FirstName = "Test FirstName";
        myMessage.LastName = "Test LastName";
        myMessage.Age = 8;
        myMessage.Emails.Add("test@test.com");
        myMessage.Emails.Add("test@test.de");

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

        // Deserialize with more properties
        var myTestMessageMoreProperties = new MyTestMessage_MoreProperties();
        myTestMessageMoreProperties.MergeFrom(serializedBytes);

        serializedBytesJson = JsonSerializer.SerializeToUtf8Bytes(myTestMessageMoreProperties, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        serializedJson = Encoding.UTF8.GetString(serializedBytesJson);
        Console.WriteLine($"Deserialized with more properties:");
        Console.WriteLine(serializedJson);
        Console.WriteLine();

        // Deserialize with less properties
        var myTestMessageLessProperties = new MyTestMessage_LessProperties();
        myTestMessageLessProperties.MergeFrom(serializedBytes);

        serializedBytesJson = JsonSerializer.SerializeToUtf8Bytes(myTestMessageLessProperties, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        serializedJson = Encoding.UTF8.GetString(serializedBytesJson);
        Console.WriteLine($"Deserialized with less properties:");
        Console.WriteLine(serializedJson);
        Console.WriteLine();

        // Deserialize with different names
        var myTestMessageDifferentNames = new MyTestMessage_DifferentNames();
        myTestMessageDifferentNames.MergeFrom(serializedBytes);

        serializedBytesJson = JsonSerializer.SerializeToUtf8Bytes(myTestMessageDifferentNames, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        serializedJson = Encoding.UTF8.GetString(serializedBytesJson);
        Console.WriteLine($"Deserialized with different names:");
        Console.WriteLine(serializedJson);
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

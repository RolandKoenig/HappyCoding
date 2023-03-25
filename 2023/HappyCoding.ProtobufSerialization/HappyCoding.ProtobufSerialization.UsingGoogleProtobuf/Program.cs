using System.Text;
using Google.Protobuf;
using Google.Protobuf.Collections;
using HappyCoding.ProtobufSerialization.UsingGoogleProtobuf.Data;
using CodedOutputStream = Google.Protobuf.CodedOutputStream;

namespace HappyCoding.ProtobufSerialization.UsingGoogleProtobuf;

internal class Program
{
    static void Main(string[] args)
    {
        var myMessage = new MyTestMessage();
        myMessage.FirstName = "Test FirstName";
        myMessage.LastName = "Test LastName";
        myMessage.Age = 8;
        myMessage.Emails.Add("test@test.com");
        myMessage.Emails.Add("test@test.de");

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

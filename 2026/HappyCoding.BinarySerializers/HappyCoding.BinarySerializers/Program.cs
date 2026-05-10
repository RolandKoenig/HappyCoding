namespace HappyCoding.BinarySerializers;

class Program
{
    static void Main(string[] args)
    {
        var testData = TestDataGenerator.CreateTestData(rowCount: 10000);
        
        var serializedData = SerializationScenarios.SerializeWithMemoryPack(testData);
        Console.WriteLine($"MemoryPack serialized payload size: {serializedData.Length:N0} bytes");

        var protobufSerializedData = SerializationScenarios.SerializeWithProtobuf(testData);
        Console.WriteLine($"Protobuf serialized payload size: {protobufSerializedData.Length:N0} bytes");

        var googleProtobufSerializedData = SerializationScenarios.SerializeWithGoogleProtobuf(testData);
        Console.WriteLine($"Google.Protobuf serialized payload size: {googleProtobufSerializedData.Length:N0} bytes");

        var jsonSerializedData = SerializationScenarios.SerializeWithSystemTextJson(testData);
        Console.WriteLine($"System.Text.Json serialized payload size: {jsonSerializedData.Length:N0} bytes");

        var messagePackSerializedData = SerializationScenarios.SerializeWithMessagePack(testData);
        Console.WriteLine($"MessagePack serialized payload size: {messagePackSerializedData.Length:N0} bytes");
    }
}
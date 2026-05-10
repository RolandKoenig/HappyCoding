using BenchmarkDotNet.Attributes;

namespace HappyCoding.BinarySerializers.Benchmarks;

[MemoryDiagnoser]
public class SerializationBenchmarks
{
    private readonly Model.ExportData _testData = TestDataGenerator.CreateTestData(rowCount: 10000);

    [Benchmark]
    public byte[] MemoryPack()
    {
        return SerializationScenarios.SerializeWithMemoryPack(_testData);
    }

    [Benchmark]
    public byte[] ProtobufNet()
    {
        return SerializationScenarios.SerializeWithProtobuf(_testData);
    }

    [Benchmark]
    public byte[] GoogleProtobuf()
    {
        return SerializationScenarios.SerializeWithGoogleProtobuf(_testData);
    }

    [Benchmark]
    public byte[] SystemTextJson()
    {
        return SerializationScenarios.SerializeWithSystemTextJson(_testData);
    }

    [Benchmark]
    public byte[] MessagePack()
    {
        return SerializationScenarios.SerializeWithMessagePack(_testData);
    }
}
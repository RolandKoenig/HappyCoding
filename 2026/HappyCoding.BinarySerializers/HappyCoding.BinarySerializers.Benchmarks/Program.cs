using BenchmarkDotNet.Running;

namespace HappyCoding.BinarySerializers.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<SerializationBenchmarks>();
    }
}

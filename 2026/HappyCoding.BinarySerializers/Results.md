## Payload size
 - MemoryPack serialized payload size: 1.917.249 bytes
 - Protobuf serialized payload size: 1.520.575 bytes
 - Google.Protobuf serialized payload size: 1.513.909 bytes
 - System.Text.Json serialized payload size: 2.487.298 bytes
 - MessagePack serialized payload size: 2.360.606 bytes

## Performance 
| Method         | Mean     | Error     | StdDev    | Gen0      | Gen1      | Gen2     | Allocated |
|--------------- |---------:|----------:|----------:|----------:|----------:|---------:|----------:|
| MemoryPack     | 1.171 ms | 0.0035 ms | 0.0033 ms |  332.0313 |  332.0313 | 332.0313 |   1.83 MB |
| ProtobufNet    | 1.284 ms | 0.0048 ms | 0.0042 ms | 1013.6719 | 1000.0000 | 998.0469 |   5.58 MB |
| GoogleProtobuf | 1.356 ms | 0.0032 ms | 0.0030 ms |  498.0469 |  498.0469 | 498.0469 |   2.61 MB |
| SystemTextJson | 2.376 ms | 0.0049 ms | 0.0046 ms |  273.4375 |  273.4375 | 273.4375 |   2.37 MB |
| MessagePack    | 1.150 ms | 0.0056 ms | 0.0049 ms |  250.0000 |  250.0000 | 250.0000 |   2.25 MB |

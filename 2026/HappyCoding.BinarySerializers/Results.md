## Payload size
 - MemoryPack serialized payload size: 1.950.581 bytes
 - Protobuf serialized payload size: 1.640.575 bytes
 - Google.Protobuf serialized payload size: 1.640.576 bytes
 - System.Text.Json serialized payload size: 2.607.298 bytes
 - MessagePack serialized payload size: 2.480.606 bytes

## Performance 
| Method         | Mean       | Error    | StdDev   | Gen0      | Gen1      | Gen2     | Allocated |
|--------------- |-----------:|---------:|---------:|----------:|----------:|---------:|----------:|
| MemoryPack     |   473.9 us |  8.71 us |  8.15 us |  310.0586 |  310.0586 | 310.0586 |   1.86 MB |
| ProtobufNet    | 1,295.5 us |  3.27 us |  2.90 us | 1015.6250 | 1000.0000 | 998.0469 |   6.02 MB |
| GoogleProtobuf | 1,323.5 us |  3.48 us |  3.26 us |  498.0469 |  498.0469 | 498.0469 |   2.73 MB |
| SystemTextJson | 2,176.0 us |  4.94 us |  4.62 us |  273.4375 |  273.4375 | 273.4375 |   2.49 MB |
| MessagePack    | 1,136.8 us | 17.41 us | 16.28 us |  332.0313 |  332.0313 | 332.0313 |   2.37 MB |
using MemoryPack;
using ProtoBuf;

namespace HappyCoding.BinarySerializers.Model;

[ProtoContract]
[MemoryPackable]
public partial record ExportData(
    [property: ProtoMember(1)]
    string Version,
    [property: ProtoMember(2)]
    DateTime ExportZeitstempel,
    [property: ProtoMember(3)]
    IReadOnlyList<ExportDataRow> Rows);
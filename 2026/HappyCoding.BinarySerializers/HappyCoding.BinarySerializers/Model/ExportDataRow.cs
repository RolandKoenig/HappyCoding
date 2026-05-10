using MemoryPack;
using ProtoBuf;

namespace HappyCoding.BinarySerializers.Model;

[ProtoContract]
[MemoryPackable(GenerateType.VersionTolerant)]
public partial record ExportDataRow(
    [property: ProtoMember(1), MemoryPackOrder(1), InternStringFormatter]
    string Kategorie,
    [property: ProtoMember(2), MemoryPackOrder(2), InternStringFormatter]
    string Thema,
    [property: ProtoMember(3), MemoryPackOrder(3), InternStringFormatter]
    string Datum, // Format yyyy-mm-dd
    [property: ProtoMember(4), MemoryPackOrder(4)]
    string TagTyp,
    [property: ProtoMember(5), MemoryPackOrder(5)]
    string ZeilenTyp,
    [property: ProtoMember(6), MemoryPackOrder(6)]
    double Zeitaufwand, // In hours
    [property: ProtoMember(7), MemoryPackOrder(7)]
    double Abgerechnet, // In hours
    [property: ProtoMember(8), MemoryPackOrder(8)]
    double BillingMultiplier,
    [property: ProtoMember(9), MemoryPackOrder(9)]
    string Kommentar);
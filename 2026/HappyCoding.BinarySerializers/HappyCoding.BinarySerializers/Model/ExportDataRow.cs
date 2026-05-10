using MemoryPack;
using ProtoBuf;

namespace HappyCoding.BinarySerializers.Model;

[ProtoContract]
[MemoryPackable]
public partial record ExportDataRow(
    [property: ProtoMember(1)]
    string Kategorie,
    [property: ProtoMember(2)]
    string Thema,
    [property: ProtoMember(3)]
    string Datum, // Format yyyy-mm-dd
    [property: ProtoMember(4)]
    string TagTyp,
    [property: ProtoMember(5)]
    string ZeilenTyp,
    [property: ProtoMember(6)]
    double Zeitaufwand, // In hours
    [property: ProtoMember(7)]
    double Abgerechnet, // In hours
    [property: ProtoMember(8)]
    double BillingMultiplier,
    [property: ProtoMember(9)]
    string Kommentar);
using ProtoBuf;

namespace HappyCoding.ProtobufSerialization.Data;

[ProtoContract]
internal class MyTestMessage_DifferentNames
{
    [ProtoMember(1)]
    public string Vorname { get; set; } = string.Empty;

    [ProtoMember(2)]
    public string Nachname { get; set; } = string.Empty;

    [ProtoMember(3)]
    public int Alter { get; set; }

    [ProtoMember(4)]
    public List<string> Emails { get; set; } = new List<string>();
}

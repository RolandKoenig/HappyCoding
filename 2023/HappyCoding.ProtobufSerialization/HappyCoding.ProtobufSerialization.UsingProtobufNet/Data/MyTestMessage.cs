using ProtoBuf;

namespace HappyCoding.ProtobufSerialization.Data;

[ProtoContract]
internal class MyTestMessage
{
    [ProtoMember(1)]
    public string FirstName { get; set; } = string.Empty;

    [ProtoMember(2)]
    public string LastName { get; set; } = string.Empty;

    [ProtoMember(3)]
    public int Age { get; set; }

    [ProtoMember(4)]
    public IReadOnlyList<string> Emails { get; set; } = Array.Empty<string>();
}

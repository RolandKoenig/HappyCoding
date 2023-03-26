using ProtoBuf;

namespace HappyCoding.ProtobufSerialization.Data;

[ProtoContract]
internal class MyTestMessage_LessProperties
{
    [ProtoMember(1)]
    public string FirstName { get; set; } = string.Empty;

    [ProtoMember(2)]
    public string LastName { get; set; } = string.Empty;

    [ProtoMember(4)]
    public List<string> Emails { get; set; } = new List<string>();
}

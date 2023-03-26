using ProtoBuf;

namespace HappyCoding.ProtobufSerialization.Data;

[ProtoContract]
internal class MyTestMessage_MoreProperties
{
    [ProtoMember(1)]
    public string FirstName { get; set; } = string.Empty;

    [ProtoMember(2)]
    public string LastName { get; set; } = string.Empty;

    [ProtoMember(3)]
    public int Age { get; set; }
    
    [ProtoMember(5)]
    public string Address { get; set; } = string.Empty;

    [ProtoMember(4)]
    public List<string> Emails { get; set; } = new List<string>();
}

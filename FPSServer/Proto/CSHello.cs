using ProtoBuf;

namespace FPSServer.Proto;

[Serializable, ProtoContract (Name = @"CSHello")]
public class CSHello:CSPacketBase
{
    public override int Id => 6;

    [ProtoMember (1)]
    public string Name { get; set; }

    public override void Clear () 
    {

    }
}
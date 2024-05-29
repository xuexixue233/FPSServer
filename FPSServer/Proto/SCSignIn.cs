using ProtoBuf;

namespace FPSServer.Proto;

[Serializable, ProtoContract(Name = @"SCSignIn")]
public class SCSignIn : SCPacketBase
{
    public override void Clear()
    {
        
    }

    public override int Id => 11;

    [ProtoMember(1)] public int State { get; set; }
}
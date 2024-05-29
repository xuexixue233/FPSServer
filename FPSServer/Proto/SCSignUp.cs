using ProtoBuf;

namespace FPSServer.Proto;

[ProtoContract]
public class SCSignUp : SCPacketBase
{
    public override void Clear()
    {
            
    }

    public override int Id => 13;

    [ProtoMember(1)] public int State;
}
using ProtoBuf;

namespace FPSServer.Proto;

[ProtoContract]
public class CSSignIn : CSPacketBase
{
    public override void Clear()
    {
        
    }

    public override int Id => 10;

    [ProtoMember(1)] public string Account;

    [ProtoMember(2)] public string Password;
}
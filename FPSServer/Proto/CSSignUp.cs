using ProtoBuf;

namespace FPSServer.Proto;

[ProtoContract]
public class CSSignUp : CSPacketBase
{
    public override void Clear()
    {
        
    }

    public override int Id => 12;

    [ProtoMember(1)]
    public string Account;
        
    [ProtoMember(2)] 
    public string Password;
}
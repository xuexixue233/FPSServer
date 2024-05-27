using ProtoBuf;

namespace FPSServer.Proto;

[Serializable, ProtoContract(Name = @"SCHeartBeat")]
public class SCHeartBeat : SCPacketBase
{
    public override int Id => 2;

    [ProtoMember(1)]
    public string time{ get; set; }

    public override void Clear()
    {
        
    }
    
    public SCHeartBeat()
    {
        time = "1";
    }
}
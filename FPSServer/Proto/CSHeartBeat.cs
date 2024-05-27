using ProtoBuf;

namespace FPSServer.Proto;

[Serializable, ProtoContract(Name = @"CSHeartBeat")]
public class CSHeartBeat : CSPacketBase
{
    public CSHeartBeat()
    {
        
    }
        
    public override int Id => 1;

    [ProtoMember (1)]
    public string time{ get; set; }

    public override void Clear()
    {
        
    }
}
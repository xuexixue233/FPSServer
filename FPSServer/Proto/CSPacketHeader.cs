namespace FPSServer.Proto;

public sealed class CSPacketHeader : PacketHeaderBase
{
    public override int Id { get; set; }
    
    public override int PacketLength { get; set; }

    public override PacketType PacketType
    {
        get
        {
            return PacketType.ClientToServer;
        }
    }
}
namespace FPSServer.Proto;

public abstract class CSPacketBase : PacketBase
{
    public override PacketType PacketType
    {
        get { return PacketType.ClientToServer; }
    }
}
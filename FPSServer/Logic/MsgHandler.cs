using FPSServer.Net;
using FPSServer.Proto;

namespace FPSServer.Logic;

public partial class MsgHandler
{
    public static void HeartBeat(ClientState state,PacketBase packetBase)
    {
        NetManager.Send(state,packetBase);
        Console.WriteLine("heartbeat");
    }
}
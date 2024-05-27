using FPSServer.Net;
using FPSServer.Proto;
using GameFramework;

namespace FPSServer.Logic;

public partial class MsgHandler
{
    public static void HeartBeat(ClientState state,PacketBase packetBase)
    {
        NetManager.Send( state,ReferencePool.Acquire<SCHeartBeat>());
        Console.WriteLine("heartbeat");
    }
}
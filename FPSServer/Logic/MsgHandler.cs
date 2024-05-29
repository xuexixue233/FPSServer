using FPSServer.db;
using FPSServer.Net;
using FPSServer.Proto;
using GameFramework;

namespace FPSServer.Logic;

public partial class MsgHandler
{
    public static void HeartBeat(ClientState state,PacketBase packetBase)
    {
        NetManager.Send(state,ReferencePool.Acquire<SCHeartBeat>());
        Console.WriteLine("heartbeat");
    }

    public static void SignIn(ClientState state, PacketBase packetBase)
    {
        var scSignIn = ReferencePool.Acquire<SCSignIn>();
        scSignIn.State = 2;

        CSSignIn csSignIn = packetBase as CSSignIn;
        
        if (csSignIn != null)
        {
            scSignIn.State = DbManager.CheckPassword(csSignIn.Account, csSignIn.Password) ? 1 : 2;
        }
        NetManager.Send(state,scSignIn);
        Console.WriteLine("SignInState:{0}",scSignIn.State);
        
        ReferencePool.Release(scSignIn);
    }

    public static void SignUp(ClientState state, PacketBase packetBase)
    { 
        var scSignUp = ReferencePool.Acquire<SCSignUp>();
        scSignUp.State = 2;

        CSSignUp csSignUp = packetBase as CSSignUp;
        
        if (csSignUp != null)
        {
            scSignUp.State = DbManager.Register(csSignUp.Account, csSignUp.Password) ? 1 : 2;
        }
        
        NetManager.Send(state,scSignUp);
        Console.WriteLine("SignInState:{0}",scSignUp.State);
        
        ReferencePool.Release(scSignUp);
    }
}
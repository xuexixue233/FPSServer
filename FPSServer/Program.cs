using System.Net.Sockets;
using FPSServer.Net;

namespace FPSServer;
class MainClass
{
    public static void Main (string[] args)
    {
        //数据库接口
        // if(!DbManager.Connect("game", "127.0.0.1", 3306, "root", "")){
        //     return;
        // }

        NetManager.StartLoop(8888);
    }
}
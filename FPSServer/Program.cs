using System.Net.Sockets;
using FPSServer.db;
using FPSServer.Net;

namespace FPSServer;
class MainClass
{
    public static void Main (string[] args)
    {
        //数据库接口
        if(!DbManager.Connect("fpsserver", "127.0.0.1", 3306, "root", "b5fe5bf42418724e")){
            return;
        }

        NetManager.StartLoop(8888);
    }
}
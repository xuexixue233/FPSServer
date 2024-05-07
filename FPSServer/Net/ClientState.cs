﻿using System.Net.Sockets;

namespace FPSServer.Net;

public class ClientState
{
    public Socket socket; 
    public ByteArray readBuff = new ByteArray(); 
    //Ping
    public long lastPingTime = 0;
    //玩家
    //public Player player;
}
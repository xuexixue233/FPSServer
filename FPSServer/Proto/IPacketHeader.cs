﻿namespace FPSServer.Proto;

public interface IPacketHeader
{
    /// <summary>
    /// 获取网络消息包长度。
    /// </summary>
    int PacketLength
    {
        get;
    }
}
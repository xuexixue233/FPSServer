﻿using ProtoBuf;

namespace FPSServer.Proto;

[ProtoContract]
public abstract class CSPacketBase : PacketBase
{
    public override PacketType PacketType
    {
        get { return PacketType.ClientToServer; }
    }
}
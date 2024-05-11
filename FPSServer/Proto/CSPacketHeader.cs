﻿using ProtoBuf;

namespace FPSServer.Proto;

[Serializable,ProtoContract(Name = @"CSPacketHeader")]
public sealed class CSPacketHeader : PacketHeaderBase
{
    [ProtoMember (1)]
    public override int Id { get; set; }
    
    [ProtoMember (2)]
    public override int PacketLength { get; set; }

    public override PacketType PacketType
    {
        get
        {
            return PacketType.ClientToServer;
        }
    }
}
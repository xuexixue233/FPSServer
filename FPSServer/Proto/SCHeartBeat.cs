﻿namespace FPSServer.Proto;

public class SCHeartBeat : SCPacketBase
{
    public SCHeartBeat()
    {
    }

    public override int Id
    {
        get
        {
            return 2;
        }
    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }
}
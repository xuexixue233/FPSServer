namespace FPSServer.Proto;

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

    public override string protoName
    {
        get => "SCHeartBeat";
        set => throw new NotImplementedException();
    }

    public override void Clear()
    {
    }
}
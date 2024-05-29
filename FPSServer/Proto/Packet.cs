using GameFramework;
using ProtoBuf;

namespace FPSServer.Proto;

[ProtoContract]
public abstract class Packet : BaseEventArgs
{
}
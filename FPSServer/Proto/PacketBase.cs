using ProtoBuf;

namespace FPSServer.Proto;

public abstract class PacketBase : IExtensible
{
    private IExtension m_ExtensionObject;

    public abstract int Id { get; }
    
    public PacketBase()
    {
        m_ExtensionObject = null;
    }

    public abstract PacketType PacketType
    {
        get;
    }

    IExtension IExtensible.GetExtensionObject(bool createIfMissing)
    {
        return Extensible.GetExtensionObject(ref m_ExtensionObject, createIfMissing);
    }

    public abstract void Clear();
}
using System.Text;
using ProtoBuf;
using ProtoBuf.Meta;

namespace FPSServer.Proto;

public class ProtoManager
{
    private Dictionary<int, Type?> m_ClientToServerPacketTypes=new Dictionary<int, Type?>();

    //编码
    public static byte[] Encode(PacketBase msgBase){
        MemoryStream stream= new MemoryStream();
        Serializer.SerializeWithLengthPrefix(stream,msgBase,PrefixStyle.Fixed32);
        return stream.ToArray();
    }

    //解码
    public static PacketBase? Decode(IPacketHeader packetHeader, byte[] bytes, int offset)
    {
        if (packetHeader is not CSPacketHeader csPacketHeader)
        {
            Console.WriteLine("");
            return null;
        }

        MemoryStream stream= new MemoryStream(bytes);
        stream.Seek(offset,SeekOrigin.Begin);
        return Serializer.Deserialize<PacketBase>(stream);
    }
    
    //编码协议名
    public static byte[] EncodeName(SCPacketHeader msgBase){
        MemoryStream stream= new MemoryStream();
        Serializer.SerializeWithLengthPrefix(stream,msgBase,PrefixStyle.Fixed32);
        return stream.ToArray();
    }

    //解码协议名(暂时为前八字节)
    public static CSPacketHeader DecodeName(byte[] bytes, int offset){
        if (bytes.Length<8)
        {
            return new CSPacketHeader();
        }
        MemoryStream stream= new MemoryStream(bytes,offset,8);
        return RuntimeTypeModel.Default.Deserialize<CSPacketHeader>(stream);
    }

    private Type? GetClientToServerPacketType(int id)
    {
        Type? type = null;
        if (m_ClientToServerPacketTypes.TryGetValue(id,out type))
        {
            return type;
        }

        return null;
    }
}
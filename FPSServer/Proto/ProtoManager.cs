using System.Reflection;
using System.Text;
using GameFramework;
using ProtoBuf;
using ProtoBuf.Meta;

namespace FPSServer.Proto;

public class ProtoManager
{
    private static Dictionary<int, Type?> m_ClientToServerPacketTypes=new Dictionary<int, Type?>();

    public static void Init()
    {
        Type packetBaseType = typeof(CSPacketBase);
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++)
        {
            if (!types[i].IsClass || types[i].IsAbstract)
            {
                continue;
            }

            if (types[i].BaseType == packetBaseType)
            {
                PacketBase packetBase = (PacketBase)Activator.CreateInstance(types[i]);
                Type packetType = GetClientToServerPacketType(packetBase.Id);
                if (packetType != null)
                {
                    Console.WriteLine("Already exist packet type '{0}', check '{1}' or '{2}'?.", packetBase.Id.ToString(), packetType.Name, packetBase.GetType().Name);
                    continue;
                }

                m_ClientToServerPacketTypes.Add(packetBase.Id, types[i]);
            }
        }
    }
    
    //编码
    public static byte[] Encode(PacketBase msgBase)
    {
        MemoryStream memoryStream = new MemoryStream();
        memoryStream.Position = 8;
        RuntimeTypeModel.Default.SerializeWithLengthPrefix(memoryStream, msgBase, msgBase.GetType(), PrefixStyle.Fixed32, 0);
        return memoryStream.ToArray();
        
    }

    //解码
    public static Packet? Decode(IPacketHeader packetHeader, byte[] bytes, int offset)
    {
        if (packetHeader is not CSPacketHeader csPacketHeader)
        {
            Console.WriteLine("PacketHeader is null");
            return null;
        }

        Type? packetType = GetClientToServerPacketType(csPacketHeader.Id);

        if (packetType!=null)
        {
            MemoryStream stream= new MemoryStream(bytes);
            stream.Seek(offset,SeekOrigin.Begin);
            return (Packet)RuntimeTypeModel.Default.DeserializeWithLengthPrefix(stream, ReferencePool.Acquire(packetType), packetType, PrefixStyle.Fixed32, 0);
        }

        return null;
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
        
        MemoryStream stream= new MemoryStream(SubByte(bytes,offset,8));
        return Serializer.DeserializeWithLengthPrefix<CSPacketHeader>(stream, PrefixStyle.Fixed32);
    }

    private static Type? GetClientToServerPacketType(int id)
    {
        Type? type = null;
        if (m_ClientToServerPacketTypes.TryGetValue(id,out type))
        {
            return type;
        }

        return null;
    }
    
    private static byte[] SubByte(byte[] srcBytes, int startIndex, int length)
    {
        MemoryStream bufferStream = new MemoryStream();
        byte[] returnByte = new byte[] { };
        if (srcBytes == null) { return returnByte; }
        if (startIndex < 0) { startIndex = 0; }
        if (startIndex < srcBytes.Length)
        {
            if (length < 1 || length > srcBytes.Length - startIndex) { length = srcBytes.Length - startIndex; }
            bufferStream.Write(srcBytes, startIndex, length);
            returnByte = bufferStream.ToArray();
            bufferStream.SetLength(0);
            bufferStream.Position = 0;
        }
        bufferStream.Close();
        bufferStream.Dispose();
        return returnByte;
    }
}
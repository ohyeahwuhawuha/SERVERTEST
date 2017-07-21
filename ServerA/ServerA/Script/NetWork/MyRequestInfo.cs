using SuperSocket.SocketBase.Protocol;
using System.IO;

namespace ServerP04
{
    class MyRequestInfo : IRequestInfo<byte[]>
    {
        public string Key { get; private set; }
        public ushort MessageType;
        public byte[] Body { get; private set; }

        public MyRequestInfo(ushort _type, byte[] _body)
        {
            MessageType = _type;            
            Body = _body;
        }
    }
}

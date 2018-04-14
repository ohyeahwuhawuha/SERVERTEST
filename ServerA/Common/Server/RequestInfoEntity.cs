using SuperSocket.SocketBase.Protocol;
using System.IO;

namespace Common
{
    public class RequestInfoEntity : IRequestInfo<byte[]>
    {
        public string Key { get; private set; }
        public ushort MessageType;
        public byte[] Body { get; private set; }

        public RequestInfoEntity(ushort _type, byte[] _body)
        {
            MessageType = _type;
            Body = _body;
        }
    }
}

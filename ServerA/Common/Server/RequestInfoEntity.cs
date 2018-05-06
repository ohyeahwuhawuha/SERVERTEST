using SuperSocket.SocketBase.Protocol;
using System.IO;

namespace Common.Server
{
    public class RequestInfoEntity : IRequestInfo<byte[]>
    {
        public string Key { get; private set; }
        public ushort MessageType { get; private set; }
        public ushort Flag { get; private set; }
        public byte[] Body { get; private set; }

        public RequestInfoEntity(ushort type, ushort flag, byte[] body)
        {
            MessageType = type;
            Flag = flag;
            Body = body;
        }
    }
}

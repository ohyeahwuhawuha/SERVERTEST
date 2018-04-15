using System;
using System.IO;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.Facility.Protocol;
using SuperSocket.Common;

namespace Common.Server
{
    public class ReceiveFilterEntity : FixedHeaderReceiveFilter<RequestInfoEntity>
    {
        public ReceiveFilterEntity()
            : base(4)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            int _size = BitConverter.ToUInt16(header, offset + 2);
            return _size;
        }

        protected override RequestInfoEntity ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            ushort _type = BitConverter.ToUInt16(header.Array, 0);
            RequestInfoEntity requestInfo = new RequestInfoEntity(_type, bodyBuffer.CloneRange(offset, length));
            return requestInfo;
        }
    }
}

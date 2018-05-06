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
            : base(6)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            int _size = BitConverter.ToUInt16(header, offset + 4);
            return _size;
        }

        protected override RequestInfoEntity ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            ushort type = BitConverter.ToUInt16(header.Array, 0);
            ushort flag = BitConverter.ToUInt16(header.Array, 2);

            RequestInfoEntity requestInfo = new RequestInfoEntity(type, flag, bodyBuffer.CloneRange(offset, length));
            return requestInfo;
        }
    }
}

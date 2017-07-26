﻿using System;
using System.IO;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.Facility.Protocol;
using SuperSocket.Common;

namespace DataSvr
{
    class MyReceiveFilter : FixedHeaderReceiveFilter<MyRequestInfo>
    {
        public MyReceiveFilter()
            : base(4)
        {
            
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            int _size = BitConverter.ToUInt16(header, offset + 2);
            return _size;
        }

        protected override MyRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            ushort _type = BitConverter.ToUInt16(header.Array, 0);            
            MyRequestInfo requestInfo = new MyRequestInfo(_type, bodyBuffer.CloneRange(offset, length));
            return requestInfo;
        }
    }
}

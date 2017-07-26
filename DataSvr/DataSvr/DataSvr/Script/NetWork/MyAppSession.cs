using SuperSocket.SocketBase;
using System.IO;
using ProtoBuf;
using System;
using System.Collections.Generic;
//using SuperSocket.SocketBase.Command;

namespace DataSvr
{
    class MyAppSession : AppSession<MyAppSession, MyRequestInfo>
    {
        private MemoryStream _memoryStream = new MemoryStream();
        
        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();        
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }

        public void OnNewRequestReceived(MyRequestInfo requestInfo)
        {            
            switch (requestInfo.MessageType)
            {
                

                default:break;
            }            
        }

        public void SendMessage<T>(ushort msgType, T body) where T : IExtensible
        {
            _memoryStream.SetLength(0L);
            _memoryStream.Position = 0;
            Serializer.Serialize<T>(_memoryStream, body);

            ushort _size = Convert.ToUInt16(_memoryStream.Length);

            byte[] _sizeBuffer = BitConverter.GetBytes(_size);
            byte[] _typeBuffer = BitConverter.GetBytes(msgType);
            byte[] bufferBody = _memoryStream.ToArray();

            _memoryStream.Position = 0;
            _memoryStream.Write(_typeBuffer, 0, 2);
            _memoryStream.Write(_sizeBuffer, 0, 2);            
            _memoryStream.Write(bufferBody, 0, bufferBody.Length);

            byte[] _buffer = _memoryStream.ToArray();
            Send(_buffer, 0, _buffer.Length);
        }
    }
}

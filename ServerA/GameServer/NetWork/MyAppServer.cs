using System.Collections.Generic;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase;
using ProtoBuf;
using System.IO;
using System;

namespace ServerP04
{
    class MyAppServer : AppServer<MyAppSession, MyRequestInfo>
    {
        MemoryStream m_memoryStream = new MemoryStream();   
        List<MyAppSession> m_kAllPlayers = new List<MyAppSession>();

        public MyAppServer()
            : base(new DefaultReceiveFilterFactory<MyReceiveFilter, MyRequestInfo>())
        {
            //NewRequestReceived += OnNewRequestReceived;
        }

        public void BroadcastMessage<T>(ushort msgType, T body) where T : IExtensible
        {
            m_memoryStream.SetLength(0L);
            m_memoryStream.Position = 0;
            ProtoBuf.Serializer.Serialize<T>(m_memoryStream, body);

            ushort _headSize = 4;
            ushort _size = (ushort)(Convert.ToUInt16(m_memoryStream.Length));

            byte[] _sizeBuffer = BitConverter.GetBytes(_size);
            byte[] _typeBuffer = BitConverter.GetBytes(msgType);
            byte[] bufferBody = m_memoryStream.ToArray();

            m_memoryStream.Position = 0;
            m_memoryStream.Write(_typeBuffer, 0, 2);
            m_memoryStream.Write(_sizeBuffer, 0, 2);
            m_memoryStream.Write(bufferBody, 0, bufferBody.Length);

            byte[] _buffer = m_memoryStream.ToArray();
            foreach (var session in GetAllSessions())
            {
                session.Send(_buffer, 0, _buffer.Length);
            }
        }

        private void OnNewRequestReceived(MyAppSession session, MyRequestInfo requestInfo)
        {
            session.OnNewRequestReceived(requestInfo);
        }

        protected override void OnNewSessionConnected(MyAppSession session)
        {
            base.OnNewSessionConnected(session);

            Console.WriteLine("OnNewSessionConnected session id = {0}", session.SessionID);
        }

        protected override void OnSessionClosed(MyAppSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);

            Console.WriteLine("OnSessionClosed session id = {0} CloseReason = {1}", session.SessionID, reason);
        }
    }
}

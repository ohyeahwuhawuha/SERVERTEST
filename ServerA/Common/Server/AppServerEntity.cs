using System.Collections.Generic;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase;
using ProtoBuf;
using Protocl;
using System.IO;
using System;

namespace Common.Server
{
    public class AppServerEntity <TAppSession> : AppServer<TAppSession, RequestInfoEntity> where TAppSession : AppSession<TAppSession, RequestInfoEntity>, IAppSession, new()
    {
        private MemoryStream _sendStream = new MemoryStream();
        private MemoryStream _receiveStream = new MemoryStream();
        private Action<TAppSession, EMessage, Stream> _action = null;

        private List<TAppSession> _sessions = new List<TAppSession>();
        private Dictionary<ushort, Action<TAppSession, EMessage, Stream>> actions = new Dictionary<ushort, Action<TAppSession, EMessage, Stream>>();

        public AppServerEntity()
            : base(new DefaultReceiveFilterFactory<ReceiveFilterEntity, RequestInfoEntity>())
        {

        }

        private byte[] GenerateMessage<T>(ushort msgType, T body) where T : IExtensible
        {
            _sendStream.SetLength(0L);
            _sendStream.Position = 0;
            Serializer.Serialize<T>(_sendStream, body);

            //ushort _headSize = 4;
            ushort _size = (ushort)(Convert.ToUInt16(_sendStream.Length));

            byte[] _sizeBuffer = BitConverter.GetBytes(_size);
            byte[] _typeBuffer = BitConverter.GetBytes(msgType);
            byte[] bufferBody = _sendStream.ToArray();

            _sendStream.Position = 0;
            _sendStream.Write(_typeBuffer, 0, 2);
            _sendStream.Write(_sizeBuffer, 0, 2);
            _sendStream.Write(bufferBody, 0, bufferBody.Length);

            return _sendStream.ToArray();
        }

        public void RegisterMessage(EMessage message, Action<TAppSession, EMessage, Stream> handler)
        {
            if (handler == null)
                return;
            actions[(ushort)message] = handler;
        }

        public void SendMessage<T>(TAppSession session, ushort msgType, T body) where T : IExtensible
        {
            if (session == null)
                return;
            byte[] _buffer = GenerateMessage<T>(msgType, body);
            session.Send(_buffer, 0, _buffer.Length);
        }

        public void BroadcastMessage<T>(ushort msgType, T body) where T : IExtensible
        {
            byte[] _buffer = GenerateMessage<T>(msgType, body);
            foreach (var session in GetAllSessions())
            {
                session.Send(_buffer, 0, _buffer.Length);
            }
        }

        protected override void ExecuteCommand(TAppSession session, RequestInfoEntity requestInfo)
        {
            if (actions.TryGetValue(requestInfo.MessageType, out _action))
            {
                _receiveStream.SetLength(0L);
                _receiveStream.Position = 0;
                _receiveStream.Write(requestInfo.Body, 0, requestInfo.Body.Length);
                _receiveStream.Position = 0;
                _action.Invoke(session, (EMessage)requestInfo.MessageType, _receiveStream);
            }
        }

        protected override void OnNewSessionConnected(TAppSession session)
        {
            base.OnNewSessionConnected(session);

            Console.WriteLine("OnNewSessionConnected session id = {0}", session.SessionID);
        }

        protected override void OnSessionClosed(TAppSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);

            Console.WriteLine("OnSessionClosed session id = {0} CloseReason = {1}", session.SessionID, reason);
        }
    }
}

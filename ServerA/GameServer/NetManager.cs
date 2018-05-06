using System;
using System.IO;
using ProtoBuf;
using Protocl;
using Common;
using Common.Server;

namespace GameServer
{
    public class NetManager : Singleton<NetManager>
    {
        private AppServerEntity<GameSession> _server = new AppServerEntity<GameSession>();
        private MySqlConnect _MySqlConnect = new MySqlConnect();

        public void OnStart()
        {
            _MySqlConnect.Initialize();
            OpenServer();
        }

        public void OnEnd()
        {
            CloseServer();
        }

        public void RegisterMessage(EMessage message, Action<GameSession, EMessage, Stream> handler)
        {
            _server.RegisterMessage(message, handler);
        }

        public void SendMessage(GameSession session, EMessage msgType, ushort flag = 0)
        {
            if (_server != null)
                _server.SendMessage(session, (ushort)msgType, flag);
        }

        public void SendMessage<T>(GameSession session, EMessage msgType, T body, ushort flag = 0) where T : IExtensible
        {
            if (_server != null)
                _server.SendMessage<T>(session, (ushort)msgType, body, flag);
        }

        public void BroadcastMessage(EMessage msgType, ushort flag = 0)
        {
            if (_server != null)
                _server.BroadcastMessage((ushort)msgType, flag);
        }

        public void BroadcastMessage<T>(EMessage msgType, T body, ushort flag = 0) where T : IExtensible
        {
            if (_server != null)
                _server.BroadcastMessage<T>((ushort)msgType, body, flag);
        }

        private void OpenServer()
        {
            if (!_server.Setup(2012))
            {
                Console.WriteLine("Failed to setup!");
                return;
            }

            if (!_server.Start())
            {
                Console.WriteLine("Failed to start!");
                return;
            }
        }

        private void CloseServer()
        {
            _server.Stop();
            Console.WriteLine("The server was stopped!");
        }        
    }
}

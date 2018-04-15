using System;
using System.IO;
using ProtoBuf;
using Protocl;
using Common;
using Common.Server;

namespace BattleServer
{
    public class NetManager : Singleton<NetManager>
    {
        private AppServerEntity _server = new AppServerEntity();

        public void OnStart()
        {
            OpenServer();
        }

        public void OnEnd()
        {
            CloseServer();
        }

        public void RegisterMessage(EMessage message, Action<AppSessionEntity, EMessage, Stream> handler)
        {
            _server.RegisterMessage(message, handler);
        }

        public void SendMessage<T>(AppSessionEntity session, EMessage msgType, T body) where T : IExtensible
        {
            if (_server != null)
                _server.SendMessage<T>(session, (ushort)msgType, body);
        }

        public void BroadcastMessage<T>(EMessage msgType, T body) where T : IExtensible
        {
            if (_server != null)
                _server.BroadcastMessage<T>((ushort)msgType, body);
        }

        private void OpenServer()
        {
            _server.NewSessionConnected += OnSessionConnected;

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

        private void OnSessionConnected(AppSessionEntity session)
        {
            
        }
    }
}

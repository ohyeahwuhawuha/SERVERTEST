using System;
using ProtoBuf;
using Common;
using Common.Database;

namespace GameServer
{
    public class NetManager : Singleton<NetManager>
    {
        private AppServerEntity _server = new AppServerEntity();
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

        public void SendMessage<T>(AppSessionEntity session, ushort msgType, T body) where T : IExtensible
        {
            if (_server != null)
                _server.SendMessage<T>(session, msgType, body);
        }

        public void BroadcastMessage<T>(ushort msgType, T body) where T : IExtensible
        {
            if (_server != null)
                _server.BroadcastMessage<T>(msgType, body);
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

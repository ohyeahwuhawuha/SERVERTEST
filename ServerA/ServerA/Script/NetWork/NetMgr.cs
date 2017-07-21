using System;
using System.IO;
using System.Collections.Generic;
using ProtoMessage;
using ProtoBuf;

namespace ServerP04
{
    public class NetMgr : Singleton<NetMgr>
    {
        MyAppServer m_server = null;

        MemoryStream m_memoryStream = new MemoryStream();

        public void OpenServer()
        {
            m_server = new MyAppServer();

            m_server.NewRequestReceived += OnNewRequestReceived;

            if (!m_server.Setup(2012))
            {
                Console.WriteLine("Failed to setup!");
                return;
            }

            if (!m_server.Start())
            {
                Console.WriteLine("Failed to start!");
                return;
            }
        }

        public void CloseServer()
        {
            m_server.Stop();
            Console.WriteLine("The server was stopped!");
        }

        private void OnNewRequestReceived(MyAppSession session, MyRequestInfo requestInfo)
        {
            session.OnNewRequestReceived(requestInfo);
        }
    }
}

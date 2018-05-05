using SuperSocket.SocketBase;
using System.IO;
using Protocl;
using ProtoBuf;
using System;
using System.Collections.Generic;
using Common.Server;
//using SuperSocket.SocketBase.Command;

namespace GameServer
{
    public class GameSession : AppSession<GameSession, RequestInfoEntity>
    {
        public int uid = 0;
        public bool isMatching = false;

        public PlayerBase playerBase = new PlayerBase();
    }
}

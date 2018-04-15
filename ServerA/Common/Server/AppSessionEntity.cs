using SuperSocket.SocketBase;
using System.IO;
using Protocl;
using ProtoBuf;
using System;
using System.Collections.Generic;
//using SuperSocket.SocketBase.Command;

namespace Common.Server
{
    public class AppSessionEntity : AppSession<AppSessionEntity, RequestInfoEntity>
    {
        public int uid = 0;
    }
}

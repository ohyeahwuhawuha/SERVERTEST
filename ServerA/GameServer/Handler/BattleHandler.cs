using System.IO;
using System.Collections.Generic;
using Common;
using Common.Server;
using Protocl;
using ProtoBuf;

namespace GameServer
{
    class BattleHandler : Singleton<BattleHandler>
    {
        public void RegisterMessage()
        {            
            NetManager.I.RegisterMessage(Protocl.EMessage.C2B_COMMAND, OnC2B_Command);
            NetManager.I.RegisterMessage(Protocl.EMessage.C2B_COMMAND, OnC2B_LoadProgress);
        }

        private void OnC2B_Command(GameSession session, EMessage message, Stream stream)
        {
            BattleRoom room = Battle.BattleRoomManager.I.GetRoom(session.BattleRoomID);
            if(room != null)
            {
                C2B_Command req = Serializer.Deserialize<C2B_Command>(stream);
                room.AddCommand(req);
            }
        }

        private void OnC2B_LoadProgress(GameSession session, EMessage message, Stream stream)
        {
            BattleRoom room = Battle.BattleRoomManager.I.GetRoom(session.BattleRoomID);
            if (room != null)
            {
                C2B_LoadProgress req = Serializer.Deserialize<C2B_LoadProgress>(stream);
                room.LoadProgress(req);
            }
        }
    }
}

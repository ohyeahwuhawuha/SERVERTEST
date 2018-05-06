using Common;
using Protocl;
using System.IO;
using System.Collections.Generic;

namespace GameServer.Handler
{
    public class MatchHandler : Singleton<MatchHandler>
    {
        private List<GameSession> _waitList = new List<GameSession>();
        private int _waitCount = 2;

        public void Register()
        {
            NetManager.I.RegisterMessage(EMessage.C2S_MATCH, OnC2S_MATCH);
            NetManager.I.RegisterMessage(EMessage.C2S_MATCH_CANCEL, OnC2S_MATCH_CANCEL);
        }

        #region handler
        private void OnC2S_MATCH(GameSession session, EMessage msg, Stream buff)
        {
            if (!session.isMatching)
            {
                _waitList.Add(session);
            }

            NetManager.I.SendMessage(session, EMessage.C2S_MATCH, (ushort)0);

            if (_waitList.Count > 2)
            {
                S2C_MatchSuccess rlt = new S2C_MatchSuccess();
                BattleRoom room = Battle.BattleRoomManager.I.CreateRoom();

                for (int i = 0; i < _waitCount; ++i)
                {
                    rlt.players.Add(session.playerBase);
                    room.AddPlayer(session);
                }

                for (int i = 0; i < _waitCount; ++i)
                {
                    NetManager.I.SendMessage<S2C_MatchSuccess>(session, EMessage.C2S_MATCH, rlt, 0);
                }

                _waitList.RemoveRange(0, _waitCount);
            }
        }

        private void OnC2S_MATCH_CANCEL(GameSession session, EMessage msg, Stream buff)
        {
            if (session.isMatching)
            {
                _waitList.Remove(session);
            }

            NetManager.I.SendMessage(session, EMessage.C2S_MATCH, (ushort)0);
        }

        #endregion

        #region function


        #endregion
    }
}

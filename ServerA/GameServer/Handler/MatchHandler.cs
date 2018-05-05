using Common.Server;
using Protocl;
using System.IO;
using System.Collections.Generic;

namespace GameServer.Handler
{
    public class MatchHandler
    {
        private List<GameSession> _matching = new List<GameSession>();
        private int _matchCount = 2;

        private void OnMatch(GameSession session, EMessage msg, Stream buff)
        {        
            if (!session.isMatching)
            {
                _matching.Add(session);   
            }
            S2C_Match rlt = new S2C_Match();
            rlt.rlt = 0;
            NetManager.I.SendMessage<S2C_Match>(session, EMessage.C2S_MATCH, rlt);

            if (_matching.Count > 2)
            {
                S2C_MatchSuccess rlt2 = new S2C_MatchSuccess();

                for(int i =0; i < _matchCount; ++i)
                {
                    rlt2.players.Add(session.playerBase);
                }

                for (int i = 0; i < _matchCount; ++i)
                {
                    NetManager.I.SendMessage<S2C_MatchSuccess>(session, EMessage.C2S_MATCH, rlt2);
                }

                _matching.RemoveRange(0, _matchCount);
            }
        }

        private void OnMatchCancel(GameSession session, EMessage msg, Stream buff)
        {
            if(session.isMatching)
            {
                _matching.Remove(session);
            }
            S2C_MatchCancel rlt = new S2C_MatchCancel();
            rlt.rlt = 0;
            NetManager.I.SendMessage<S2C_MatchCancel>(session, EMessage.C2S_MATCH, rlt);
        }


    }
}

using Protocl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class BattleRoom
    {
        private static int count;

        private B2C_Command commands = new B2C_Command();
        private B2C_LoadProgress progresses = new B2C_LoadProgress();

        public int uid { private set; get; }
        private List<GameSession> players = new List<GameSession>();
        private bool IsStart = false;

        public BattleRoom()
        {
            ++count;
            uid = count;
        }

        public void AddPlayer(GameSession session)
        {
            session.BattleRoomID = uid;
            players.Add(session);
        }

        public void AddCommand(C2B_Command command)
        {
            commands.commands.Add(command);
        }

        public void LoadProgress(C2B_LoadProgress req)
        {
            if (req == null)
                return;

            bool newReq = true;

            for (int i = 0; i < progresses.progresses.Count; ++i)
            {
                if (progresses.progresses[i].uid == req.uid)
                {
                    progresses.progresses[i].progress = req.progress;
                    newReq = false;
                    break;
                }
            }
            if (newReq)
            {
                progresses.progresses.Add(req);
            }

            IsStart = CheckAllLoaded();
            if (IsStart)
            {
                NetManager.I.BroadcastMessage(EMessage.B2C_ALL_LOADED);
            }
            else
            {
                NetManager.I.BroadcastMessage<B2C_LoadProgress>(EMessage.B2C_LOAD_PROGRESS, progresses);
            }
        }

        public bool CheckAllLoaded()
        {
            for (int i = 0; i < progresses.progresses.Count; ++i)
            {
                if (progresses.progresses[i].progress < 100)
                {
                    return false;
                }
            }

            return true;
        }

        public void Clear()
        {
            players.Clear();
            commands.commands.Clear();
            progresses.progresses.Clear();
            for (int i = 0; i < players.Count; ++i)
            {
                players[i].BattleRoomID = 0;
            }
        }

        public void Update(long dt)
        {
            if (IsStart)
            {
                commands.dt = dt;
                for (int i = 0; i < players.Count; ++i)
                {
                    NetManager.I.SendMessage<B2C_Command>(players[i], EMessage.B2C_COMMAND, commands);
                }
                commands.commands.Clear();
            }
        }
    }
}

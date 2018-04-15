using System.IO;
using System.Collections.Generic;
using Common;
using Common.Server;
using Protocl;
using ProtoBuf;

namespace BattleServer
{
    class LogicHandler : Singleton<LogicHandler>
    {
        private B2C_Command commands = new B2C_Command();
        private B2C_LoadProgress progresses = new B2C_LoadProgress();
        
        private bool IsStart = false;

        public void RegisterMessage()
        {
            NetManager.I.RegisterMessage(Protocl.EMessage.C2B_LOAD_PROGRESS, null);
            NetManager.I.RegisterMessage(Protocl.EMessage.C2B_COMMAND, null);
        }

        public void OnUpdate(long dt)
        {
            if(IsStart)
            {
                commands.dt = dt;
                NetManager.I.BroadcastMessage<B2C_Command>(EMessage.B2C_COMMAND, commands);
                commands.commons.Clear();
            }
            else if (progresses.progresses.Count > 0)
            {
                NetManager.I.BroadcastMessage<B2C_LoadProgress>(EMessage.B2C_LOAD_PROGRESS, progresses);
                progresses.progresses.Clear();
            }           
        }

        private void OnC2B_Command(AppSessionEntity session, EMessage message, Stream stream)
        {
            C2B_Command req = Serializer.Deserialize<C2B_Command>(stream);
            if (req == null)
                return;
            commands.commons.Add(req);
        }

        private void OnC2B_LoadProgress(AppSessionEntity session, EMessage message, Stream stream)
        {
            C2B_LoadProgress req = Serializer.Deserialize<C2B_LoadProgress>(stream);
            if (req == null)
                return;

            for(int i = 0; i < progresses.progresses.Count; ++i)
            {
                if(progresses.progresses[i].uid == req.uid)
                {
                    progresses.progresses[i].progress = req.progress;
                    return;
                }                
            }
            progresses.progresses.Add(req);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace GameServer.Battle
{
    public class BattleRoomManager : Singleton<BattleRoomManager>
    {
        private Dictionary<int, BattleRoom> rooms = new Dictionary<int, BattleRoom>();
        private Queue<BattleRoom> roomPool = new Queue<BattleRoom>();

        public BattleRoom CreateRoom()
        {
            BattleRoom room = null;
            if(roomPool.Count > 0)
            {
                room = roomPool.Dequeue();
            }
            else
            {
                room = new BattleRoom();
            }            
            rooms.Add(room.uid, room);
            return room;
        }

        public BattleRoom GetRoom(int uid)
        {
            BattleRoom room;
            if(rooms.TryGetValue(uid, out room))
            {
                return room;
            }

            return null;
        }

        public void RecoveryRoom(BattleRoom room)
        {
            if (room == null)
                return;

            room.Clear();
            rooms.Remove(room.uid);
            roomPool.Enqueue(room);
        }

        public void Update(long dt)
        {
            foreach(BattleRoom room in rooms.Values)
            {
                room.Update(dt);
            }
        }
    }
}

using System;
using log4net;
using Common.Time;

namespace GameServer
{
    class AppMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the server!");
            Console.ReadKey();

            NetManager.I.OnStart();

            Console.WriteLine("The server started successfully !!! \n press key 'q' to stop it!");

            Timer.SetStartTime();
            long preTime = Timer.GetTime();
            long deltaTime = 0L;

            while (Console.ReadKey().KeyChar != 'q')
            {
                deltaTime += (Timer.GetTime() - preTime);
                preTime = Timer.GetTime();
                if (deltaTime >= 20)
                {
                    Battle.BattleRoomManager.I.Update(deltaTime);
                    deltaTime = 0L;
                }
            }

            NetManager.I.OnEnd();
           
            Console.WriteLine("Press any key to end app!");
            Console.ReadKey();
        }
    }
}

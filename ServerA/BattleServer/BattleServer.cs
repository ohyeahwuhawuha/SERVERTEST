using System;
using Common.Time;

namespace BattleServer
{
    class BattleServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the server!");
            Console.ReadKey();

            NetManager.I.OnStart();

            Console.WriteLine("The server started successfully !!! \n press key 'q' to stop it!");

            long startTime = Timer.GetTime();
            long currentTime = startTime;
            long dt = 0L;
            long fps = 30;
            long ft = 1000 / fps;

            LogicHandler.I.RegisterMessage();

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                dt = currentTime - startTime;
                if(dt >= ft)
                {
                    LogicHandler.I.OnUpdate(dt);
                }
                continue;
            }

            NetManager.I.OnEnd();
           
            Console.WriteLine("Press any key to end app!");
            Console.ReadKey();
        }
    }
}

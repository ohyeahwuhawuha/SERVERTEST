using System;
using log4net;

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
            
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            NetManager.I.OnEnd();
           
            Console.WriteLine("Press any key to end app!");
            Console.ReadKey();
        }



    }
}

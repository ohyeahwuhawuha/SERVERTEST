using System;

namespace ServerP04
{
    class AppMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the server!");
            Console.ReadKey();

            NetMgr.I.OpenServer();

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");
            
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            NetMgr.I.CloseServer();
           
            Console.WriteLine("Press any key to end app!");
            Console.ReadKey();
        }
    }
}

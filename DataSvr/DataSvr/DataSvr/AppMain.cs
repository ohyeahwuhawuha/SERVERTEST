using DataSvr.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSvr
{
    class AppMain
    {
        private static void Test()
        {
            //List<List<string>> tttt = new List<List<string>>();
            MySqlConnect.I.NotSelectAction("update level set cid = 'what' where id = '0fada017-c9bc-4dbc-8954-8ae5f0cc0b12'");
            List<List<string>> tttt = MySqlConnect.I.SelectAction("select id, cid from level");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            MySqlConnect.I.Initialize();
            NetMgr.I.OpenServer();
            Console.WriteLine("The server started successfully, press key 'q' to stop it!");
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }
            NetMgr.I.CloseServer();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace TmpServ
{
    class Program
    {
        public static bool isUpdated = false;

        public static int port = 6000;
        public static int state = 0;
        public static MapServ curm;
              
       
        static void Main(string[] args)
        {
            curm = new MapServ();
            //Map Loading
            
            Console.WriteLine("Type \"gennow N\" to generate map with roughly N/2 empty spaces");
            Console.WriteLine("Type \"load *.txt\" to load map from file");
            for (int i = 0; i < 10; ++i)
                Console.WriteLine("TURN OFF UR FUCKING FIREWALL!!!");
            //string comm = "C:\\Users\\Artem\\Documents\\tmp.txt";
            string comm = "C:\\Temp\\tmp.lab";
            curm.GenerateMapNow();
            curm.notif = "Hi!";
            /*
            if (comm.Split()[0] == "gennow")
                curm.GenerateMapNow(int.Parse(comm.Split()[1]));
            else
                if (comm.Split()[0] == "load")
                    curm.GenerateMapFromFile(comm.Split()[1]);
                else
                    throw new Exception("Fuck u anyway!");
             */
            state = 1;
            Netw cur_netw = new Netw(curm, port);
            Console.WriteLine(cur_netw.myIP);
            Thread net_th = new Thread(cur_netw.Listen);
            net_th.Start();

            while (Console.ReadLine() != "Stop");

            net_th.Abort();
            Send endmes = new Send(cur_netw.s);
            cur_netw.GetOutOfThem(endmes);
            Console.WriteLine("End");
            Thread.Sleep(100);
            
            System.Environment.Exit(0);
        }
    }
}

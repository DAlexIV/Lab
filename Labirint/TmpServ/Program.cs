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

        public static int state = 0;
        public static MapServ curm;
        
        static IPEndPoint ep;
      
       
        static void Main(string[] args)
        {
            curm = new MapServ();
            //Map Loading
            Console.WriteLine("Type \"gennow N\" to generate map with roughly N/2 empty spaces");
            Console.WriteLine("Type \"load *.txt\" to load map from file");
            //string comm = "C:\\Users\\Artem\\Documents\\tmp.txt";
            string comm = "C:\\Temp\\tmp.txt";
            curm.GenerateMapFromFile(comm);
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
            Thread net_th = new Thread(Netw.Listen);
            net_th.Start();
            while (Console.ReadLine() != "Stop");
            state = 2;
            Console.WriteLine("End");
            Thread.Sleep(100);
            net_th.Abort();
            System.Environment.Exit(0);
        }
    }
}

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
            Console.WriteLine("Type \"gennow N\" to generate map with roughly N/2 empty spaces");
            Console.WriteLine("Type \"load *.txt\" to load map from file");
            string comm = Console.ReadLine();
            
            curm.GetMap = curm.generate_map();      
             
            //Fill
            curm.cur_players = 2;
            curm.players_names[0] = "Alex";
            curm.players_names[1] = "Artyom";
            curm.players_signs[0] = 'X';
            curm.players_signs[1] = 'E';
            curm.notif = "Let's go!";
            Thread.Sleep(2000);

            Netw.Sender(ep, curm);
            Console.WriteLine("Sent!");
            Console.ReadKey();
        }
    }
}

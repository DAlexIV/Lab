using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    public class UDPListener
    {
        public static Client.MapCl Map = new Client.MapCl();
        public static Client.PlayerCl Player = new Client.PlayerCl();
        private const int listenPort = 6000;
        static public Netw cur_netw;

        public static int Main()
        {
            Console.WindowHeight = 35;
            Console.WindowWidth = 100;
            Console.CursorVisible = false;

            //Temporary block of code (just for testing)

            //Reading player id
            cur_netw = new Netw(listenPort);
            Console.WriteLine("Enter IP: ");
            string line = Console.ReadLine();
            cur_netw.Set_Serv_Ip(ref line);
            /*
            Console.WriteLine("Enter player name: ");
            Player.Name = Console.ReadLine();
            Console.WriteLine("Enter player sign: ");
            Player.M = Console.ReadKey().KeyChar;
            Console.WriteLine("Enter pair of coords: ");
            string tmpline = Console.ReadLine();
            Player.X = int.Parse(tmpline.Split()[0]);
            Player.Y = int.Parse(tmpline.Split()[1]);
            */
            Console.WriteLine("Enter your name");
            Player.Name = Console.ReadLine();
            cur_netw.Send_Token(Player); //Send player id
            //End of temp code
            Thread list_thread = new Thread(cur_netw.Listen); //Start listening
            
            list_thread.Start();

            Client.InterfaceCl0.init();
            while (true)
            {
                Client.GenInterCl.redraw();
                Client.GenInterCl.controls();
            }
            Console.ReadKey();
            return 0;
        }
    }
}
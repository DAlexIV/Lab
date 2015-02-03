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
        private const int listenPort = 11000;

        public static int Main()
        {
            Console.WindowHeight = 35;
            Console.WindowWidth = 100;
            Console.CursorVisible = false;

            //Temporary block of code (just for testing)
            Console.WriteLine("Enter IP: ");
            Netw.servIP = Console.ReadLine();
            Console.WriteLine("Enter player name: ");
            Player.Name = Console.ReadLine();
            Console.WriteLine("Enter player sign: ");
            Player.M = Console.ReadKey().KeyChar;
            Console.WriteLine("Enter pair of coords: ");
            string tmpline = Console.ReadLine();
            Player.X = int.Parse(tmpline.Split()[0]);
            Player.Y = int.Parse(tmpline.Split()[1]);
            //End of temp code

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
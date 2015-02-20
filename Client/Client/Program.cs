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
            
            Console.WriteLine("Enter IP: ");
            string line = Console.ReadLine();
            cur_netw = new Netw(listenPort, line);
            cur_netw.TestPing();
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
            Player.id = cur_netw.GetNumberOfPlayersOnServ();
            //End of temp codes
            Thread list_thread = new Thread(cur_netw.Listen); //Start listening
            
            list_thread.Start();
            Thread.Sleep(100);
            FindYourself();
            Client.InterfaceCl0.init();
            //Client.GenInterCl.redraw();
            CastBigMapToZeros();
            while (true)
            {
                Client.GenInterCl.redraw();  
                Client.GenInterCl.controls();
                          
            }
            Console.ReadKey();
            return 0;
        }

        private static void CastBigMapToZeros()
        {
            for (int i = 0; i < Map.Map.GetLength(0); i++)
                for (int j = 0; j < Map.Map.GetLength(1); j++)
                    Map.map[i, j] = 0;
        }

        private static void FindYourself()
        {
            for (int i = 0; i < Map.Map.GetLength(0); i++)
                for (int j = 0; j < Map.Map.GetLength(1); j++)
                    if (Map.Map[i, j] == Player.M)
                    {
                        Player.X = j;
                        Player.Y = i;
                    }
        }
    }
}
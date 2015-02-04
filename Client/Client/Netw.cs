using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Client
{
    public class Netw
    {
        
        static bool isEndPack = false;
        public static IPEndPoint servIP = new IPEndPoint(IPAddress.Any, listenPort);
        //static System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt", true);
        static int listenPort = 11000;
        static Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        static UdpClient listener = new UdpClient(listenPort);
        static IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        static IPEndPoint tmp = new IPEndPoint(IPAddress.Any, IPEndPoint.MaxPort);
        public static void Set_Serv_Ip(ref string line) //Устанавливает IP сервера
        {
            servIP.Address = IPAddress.Parse(line);
            servIP.Port = 11000;
        }
        public static void Send_Coords(PlayerCl pl) //Отсылает координаты
        {
            byte[] reg = { 2 };
            byte[] coords = { (byte)pl.X, (byte)pl.Y };
            soc.SendTo(reg, servIP);
            soc.SendTo(coords, servIP);
        }
        public static void Send_Token(PlayerCl pl) //Отсылает класс Player в начале игры
        {
            byte[] reg = { 1 };
            byte[] coords = {(byte)pl.X, (byte)pl.Y, (byte)(pl.M)};
            soc.SendTo(reg, servIP);
            Thread.Sleep(1);
            soc.SendTo(coords, servIP);
            Thread.Sleep(1);
            soc.SendTo(Encoding.ASCII.GetBytes(pl.Name), servIP);
        }
        public static void Send_ExitMes() //Отсылает сообщение о выходе
        {
            byte[] mes = {255};
            soc.SendTo(mes, servIP);
        }
        private static byte[] Reciever()
        {
            Console.WriteLine("Listening" + groupEP.ToString());
            byte[] bytes = listener.Receive(ref groupEP);
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length).Length);
            Console.WriteLine(groupEP.Address);
            return bytes;
        }
        public static void Listen()
        {
            //file = 
            while (!isEndPack)
            {
                Listener();
            }
        }
        private static void Listener()
        {
            Console.WriteLine("Waiting for broadcast");
            StreamWriter str = File.AppendText("C:\\Temp\\tmp.txt");
            //StreamWriter str = File.AppendText("C:\\Users\\Artem\\Documents\\newtesting.txt");
            using (str)
            {
                str.WriteLine("Waiting for broadcast");
            }
               // System.Environment.Exit(0);
                byte[] tmp = Reciever();
                Console.WriteLine(tmp.Length);
                using (str)
                {
                    str.WriteLine(tmp.Length);
                }
                UDPListener.Map.cur_players = (int)tmp[0];
                if (UDPListener.Map.cur_players != 255)
                {
                    UDPListener.Map.ByteToMap(Reciever());
                    Console.WriteLine("Recieved Map");
                    using (str)
                    {
                        str.WriteLine("Recieved Map");
                    }
                    UDPListener.Map.ByteToCharArr(Reciever());
                    Console.WriteLine("Recieved char arr");
                    using (str)
                    {
                        str.WriteLine("Recieved char arr");
                    }
                    UDPListener.Map.notif = UDPListener.Map.ByteToString(Reciever());
                    Console.WriteLine("Recieved notif");
                    using (str)
                    {
                        str.WriteLine("Recieved notif");
                    }
                    string[] ret = new string[UDPListener.Map.cur_players];
                    for (int i = 0; i < UDPListener.Map.cur_players; ++i)
                        ret[i] = UDPListener.Map.ByteToString(Reciever());
                    Console.WriteLine("Recieved names");
                    using (str)
                    {
                        str.WriteLine("Recieved names");
                    }
                    UDPListener.Map.players_names = ret;
                   // str.Close();
                }

                else
                    isEndPack = true;
            
        }
    }
}

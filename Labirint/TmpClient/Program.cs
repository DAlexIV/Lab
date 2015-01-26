using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace TmpClient
{
    class Program
    {
        static MapCl curm;
        static string servIP = "192.168.0.103";
        static int listenPort = 12000;
        static IPEndPoint ep;
        static bool is_input = false;
        static Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        static UdpClient listener = new UdpClient(listenPort);
        static IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        static IPEndPoint tmp = new IPEndPoint(IPAddress.Any, IPEndPoint.MaxPort);
        static byte[] Reciever()
        {
            byte[] bytes = listener.Receive(ref groupEP);
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length).Length);
            Console.WriteLine(groupEP.Address);
            return bytes;
        }
        
        static IPEndPoint Listener()
        {
            Console.WriteLine("Waiting for broadcast");
            curm.cur_players = Reciever()[0];
            curm.ByteToMap(Reciever());
            curm.ByteToCharArr(Reciever());
            curm.notif = curm.ByteToString(Reciever());

            string[] ret = new string[curm.cur_players];
            for (int i = 0; i < curm.cur_players; ++i)
                ret[i] = curm.ByteToString(Reciever());
            return groupEP;
        }
        static void Main(string[] args)
        {
            IPEndPoint serv = new IPEndPoint(IPAddress.Parse("192.168.0.103"), 11000);
            soc.SendTo(Encoding.ASCII.GetBytes(servIP), serv);
            Console.WriteLine("IP sent");
            Listener();
            for (int i = 0; i < MapCl.map_height; ++i)
            {
                for (int k = 0; k < MapCl.map_width; ++k)
                    Console.Write(curm.map[i, k]);
                Console.WriteLine();
            }
            for (int i = 0; i < curm.cur_players; ++i)
                Console.WriteLine(curm.players_names[i]);
            Console.WriteLine(curm.notif);
        }
    }
}

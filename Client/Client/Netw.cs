using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Netw
    {
        static MapCl curm = new MapCl();
        static bool isEndPack = false;
        public static string servIP;

        static int listenPort = 11000;
        static Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        static UdpClient listener = new UdpClient(listenPort);
        static IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        static IPEndPoint tmp = new IPEndPoint(IPAddress.Any, IPEndPoint.MaxPort);
        static void Send_Token()
        {
            
        }
        static byte[] Reciever()
        {
            Console.WriteLine("Listening" + groupEP.ToString());
            byte[] bytes = listener.Receive(ref groupEP);
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length).Length);
            Console.WriteLine(groupEP.Address);
            return bytes;
        }
        static void Listen()
        {
            while (!isEndPack)
            {
                Listener();
            }
        }
        static void Listener()
        {
            Console.WriteLine("Waiting for broadcast");
            byte[] tmp = Reciever();
            Console.WriteLine(tmp.Length);
            curm.cur_players = (int)tmp[0];
            if (curm.cur_players != 0)
            {
                curm.ByteToMap(Reciever());
                Console.WriteLine("Recieved Map");
                curm.ByteToCharArr(Reciever());
                Console.WriteLine("Recieved char arr");
                curm.notif = curm.ByteToString(Reciever());
                Console.WriteLine("Recieved notif");

                string[] ret = new string[curm.cur_players];
                for (int i = 0; i < curm.cur_players; ++i)
                    ret[i] = curm.ByteToString(Reciever());
                Console.WriteLine("Recieved names");
                curm.players_names = ret;
            }
            else
                isEndPack = true;
        }
    }
}

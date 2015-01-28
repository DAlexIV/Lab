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
        static MapServ curm;
        static int listenPort = 11000;
        static IPEndPoint ep;
        static Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        static UdpClient listener = new UdpClient(listenPort);
        static IPEndPoint Listener() //Sets connection up
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            
            IPEndPoint tmp = new IPEndPoint(IPAddress.Any, IPEndPoint.MaxPort);

            Console.WriteLine("Waiting for broadcast");
            byte[] bytes = listener.Receive(ref groupEP);
            groupEP.Port = 11000;
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            Console.WriteLine(groupEP.Address);
            return groupEP;
        }
        static void Sender(IPEndPoint curip, MapServ cur)
        {
            byte[] num = { (byte)curm.cur_players };
            s.SendTo(num, curip);
            s.SendTo(cur.EncodingIntArrToByteStream(), curip);
            s.SendTo(cur.EncodingCharArrToByteStream(), curip);
            s.SendTo(cur.EncodingStringToByteStream(curm.notif), curip);
            byte[][] names = cur.EncodingArrStringToByteStream();
            for (int i = 0; i < curm.cur_players; ++i)
                s.SendTo(names[i], curip);
        }
        static void Main(string[] args)
        {
            curm = new MapServ();
         /*   for (int i = 0; i < 10; i++)
            {
                byte[] a = { 0, 1, 1 };
                s.SendTo(a, new IPEndPoint(IPAddress.Parse("172.19.32.48"), 11000));
                Thread.Sleep(2000);
            }*/
            ep = Listener();
            curm.GetMap = curm.generate_map();      
             
            //Fill
            curm.cur_players = 2;
            curm.players_names[0] = "Alex";
            curm.players_names[1] = "Artyom";
            curm.players_signs[0] = 'X';
            curm.players_signs[1] = 'E';
            curm.notif = "Let's go!";
            Thread.Sleep(2000);

            Sender(ep, curm);
            Console.WriteLine("Sent!");
            Console.ReadKey();
        }
    }
}

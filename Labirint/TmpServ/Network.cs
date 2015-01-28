using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace TmpServ
{
    class Netw
    {
        static List<PlayerServ> pls;
        static int listenPort = 11000;
        static Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        static UdpClient listener = new UdpClient(listenPort);
        static int FindIP(IPEndPoint ip)
        {
            for (int i = 0; i < pls.Count(); ++i)
                if (pls[i].IP == ip)
                    return i;
            return -1;
        }
        static IPEndPoint Listener(MapServ cur) //Sets connection up
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            
            IPEndPoint tmp = new IPEndPoint(IPAddress.Any, IPEndPoint.MaxPort);
            byte[] mestype = listener.Receive(ref groupEP); 
            if (mestype.Length != 1)
                throw new Exception("Mestype package fail");
            switch (mestype[0])
            {
                case 1:
                    byte[] mes = listener.Receive(ref groupEP);
                    if (mestype.Length != 3) 
                        throw new Exception("New player package 1 fail");
                    byte[] strmes = listener.Receive(ref groupEP);
                    PlayerServ newpl = Decoding.BToPlayer(mes, strmes);
                    newpl.IP = groupEP;
                    pls.Add(Decoding.BToPlayer(mes, strmes));
                    break;
                case 2:
                    byte[] mes = listener.Receive(ref groupEP); 
                    if (mestype.Length != 2)
                        throw new Exception("Update package fail");
                    if (FindIP(groupEP) == -1)
                        throw new Exception("UNKNOWN IP, WTF MAN???");
                    else
                    {
                        
                    }
            }
            Console.WriteLine("Waiting for broadcast");
            byte[] bytes = listener.Receive(ref groupEP);
            groupEP.Port = 11000;
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            Console.WriteLine(groupEP.Address); 
            return groupEP;
        }
        static void SendAll(MapServ cur, List<PlayerServ> pls)
        {
            for (int i = 0; i < pls.Count; ++i)
                Sender(new IPEndPoint(IPAddress.Parse(pls[i].IP), 11000), cur);
            Console.WriteLine("Sent to all");
        }
        static void Sender(IPEndPoint curip, MapServ cur)
        {
            byte[] num = { (byte)cur.cur_players };
            s.SendTo(num, curip);
            Thread.Sleep(5);
            s.SendTo(EncodingB.EncodingIntArrToByteStream(cur), curip);
            Thread.Sleep(5);
            s.SendTo(EncodingB.EncodingCharArrToByteStream(cur), curip);
            Thread.Sleep(5);
            s.SendTo(EncodingB.EncodingStringToByteStream(cur.notif), curip);
            byte[][] names = EncodingB.EncodingArrStringToByteStream(cur);
            for (int i = 0; i < cur.cur_players; ++i)
                s.SendTo(names[i], curip);
        }
    }
}

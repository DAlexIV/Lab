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
        static void ConOut(byte[] bytes, IPEndPoint groupEP)
        {
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length));
        }
        void Listen()
        {
            while (Program.state != 2)
                ListenStep(Program.curm);
        }
        static void ListenStep(MapServ cur) //Sets connection up
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            
            IPEndPoint tmp = new IPEndPoint(IPAddress.Any, IPEndPoint.MaxPort);
            byte[] mestype = listener.Receive(ref groupEP); 
            if (mestype.Length != 1)
                throw new Exception("Mestype package fail");
            ConOut(mestype, groupEP);
            switch (mestype[0])
            {
                case 1: //Add player
                    byte[] mes = listener.Receive(ref groupEP);
                    ConOut(mes, groupEP);
                    if (mes.Length != 3) 
                        throw new Exception("New player package 1 fail");
                    byte[] strmes = listener.Receive(ref groupEP);
                    ConOut(strmes, groupEP);
                    PlayerServ newpl = Decoding.BToPlayer(mes, strmes);
                    newpl.IP = groupEP;
                    pls.Add(Decoding.BToPlayer(mes, strmes));
                        break;
                case 2: //Move player
                    byte[] mes2 = listener.Receive(ref groupEP);
                    ConOut(mes2, groupEP);
                    if (mes2.Length != 2)
                        throw new Exception("Update package fail");
                    int curpl = FindIP(groupEP);
                    if (curpl == -1)
                        throw new Exception("UNKNOWN IP, WTF MAN???");
                    else
                    {
                        pls[curpl].X = mes2[0];
                        pls[curpl].Y = mes2[1];
                    }
                    break;
                case 255: //Delete player
                    int curpl2 = FindIP(groupEP);
                    if (curpl2 == -1)
                        throw new Exception("UNKNOWN IP, WTF MAN???");
                    else
                        pls.RemoveAt(curpl2);
                    break;
                default:
                    throw new Exception("Unknown type of package");
            }
            Program.isUpdated = true;
            groupEP.Port = 11000;
        }
        static void SendAll(MapServ cur, List<PlayerServ> pls)
        {
            for (int i = 0; i < pls.Count; ++i)
                Sender(pls[i].IP, cur);
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

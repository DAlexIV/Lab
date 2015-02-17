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
        MapServ cur;
        bool just_started = true;
        List<PlayerServ> pls = new List<PlayerServ>();
        List<int> isConnected_old = new List<int>();
        List<int> isConnected = new List<int>();
        int listenPort;
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient listener;

        public Netw(MapServ cur, int port)
        {
            listenPort = port;
            this.cur = cur;
            listener = new UdpClient(port);
        }
        private string EpicOutPut(string mes)
        {
            string lines = "";
            for (int i = 0; i < 10; ++i)
                for (int k = 0; k < 50; ++k)
                    lines += "/";
            lines += mes;
            for (int i = 0; i < 10; ++i)
                for (int k = 0; k < 50; ++k)
                    lines += "/";
            return lines;
        }
        private void Checker(object state)
        {
            if (!just_started)
            {
                for (int i = 0; i < isConnected.Count(); ++i)
                {
                    if (isConnected[i] != isConnected_old[i])
                    {
                        isConnected[i] = isConnected_old[i];

                    }
                    else
                        DeletePlayer(i);
                }
            }
            Console.Beep(300, 5000);
        }
        private int FindIP(IPEndPoint ip)
        {
            for (int i = 0; i < pls.Count(); ++i)
                if (IPAddress.Equals(pls[i].IP.Address, ip.Address))
                    return i;
            return -1;
        }
        private void ConOut(byte[] bytes, IPEndPoint groupEP)
        {
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length));
        }
        public void Listen()
        {
            Send sendmess = new Send(s);
            while (Program.state != 2)
            {
                try
                {

                    ListenStep(cur);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(EpicOutPut(ex.Message));
                }
                if (!just_started)
                {
                    just_started = false;
                }
                sendmess.SendAll(ref cur, pls);
            }
            // isConn.Dispose();
            sendmess.SendEndingMessageToAll(pls);

        }
        private void ListenStep(MapServ cur) //Sets connection up
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);


            IPEndPoint tmp = new IPEndPoint(IPAddress.Any, IPEndPoint.MaxPort);
            Console.WriteLine("Server is running");
            byte[] mestype = listener.Receive(ref groupEP);
            Console.WriteLine("Recieved smt");
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
                    AddPlayer(groupEP, mes, strmes);
                    break;
                case 2: //Move player
                    byte[] mes2 = listener.Receive(ref groupEP);
                    groupEP.Port = listenPort;
                    ConOut(mes2, groupEP);
                    if (mes2.Length != 2)
                        throw new Exception("Update package fail");
                    int curpl = FindIP(groupEP);

                    if (curpl == -1)
                        throw new Exception("UNKNOWN IP, WTF MAN???");
                    else
                    {
                        cur[pls[curpl].Y, pls[curpl].X] = 0; //Delete old player
                        pls[curpl].X = mes2[0];
                        pls[curpl].Y = mes2[1];
                        cur[pls[curpl].Y, pls[curpl].X] = -curpl - 1; //Make new player
                        ++isConnected[curpl];
                    }
                    break;
                case 255: //Delete player
                    int curpl2 = FindIP(groupEP);
                    groupEP.Port = listenPort;
                    if (curpl2 == -1)
                        throw new Exception("UNKNOWN IP, WTF MAN???");
                    else DeletePlayer(curpl2);
                    break;
                default:
                    throw new Exception("Unknown type of package");
            }
        }

        private void AddPlayer(IPEndPoint groupEP, byte[] mes, byte[] strmes)
        {
            PlayerServ newpl = Decoding.BToPlayer(mes, strmes);
            groupEP.Port = listenPort;
            newpl.IP = groupEP;
            pls.Add(newpl);
            isConnected.Add(0);
            isConnected_old.Add(0);
        }

        private void DeletePlayer(int curpl2)
        {
            pls.RemoveAt(curpl2);
            isConnected.RemoveAt(curpl2);
            isConnected_old.RemoveAt(curpl2);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TmpServ
{
    class Program
    {
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
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
            Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            Console.WriteLine(groupEP.Address);
            return groupEP;
        }
        void Sender(IPEndPoint curip)
        {

        }
        static void Main(string[] args)
        {
            MapServ curm = new MapServ();
            curm.GetMap = curm.generate_map();
            ep = Listener();
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static int listenPort = 11000;
    static IPEndPoint ep;
    static bool is_input = false;
    static Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    static UdpClient listener = new UdpClient(listenPort);
    static void Thre()
    {
        while (true)
        {
            ep = new IPEndPoint(Listener().Address, 11000);
            is_input = true;
        }
    }
    static IPEndPoint Listener()
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
    static void Main(string[] args)
    {
        
        string line = Console.ReadLine();
        byte[] sendbuf = Encoding.ASCII.GetBytes(line);

        Thread Ls = new Thread(Thre);
        Ls.Start();
        while (true)
        {
            if (is_input)
            {
                s.SendTo(sendbuf, ep);
                is_input = false;
            }
            else
                Thread.Sleep(100);
        }
    }
}
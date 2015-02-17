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
        
        bool isEndPack = false;
        public IPEndPoint servIP ;
        //  System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt", true);
        int listenPort = 6000;
        Socket soc;
        UdpClient listener;
        IPEndPoint groupEP;
        public Netw(int port)
        {
            this.listenPort = port;
            servIP = new IPEndPoint(IPAddress.Any, listenPort);
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            listener = new UdpClient(listenPort);
            groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        }
        public void Set_Serv_Ip(ref string line) //Устанавливает IP сервера
        {
            servIP.Address = IPAddress.Parse(line);
            servIP.Port = listenPort;
        }
        public void Send_Coords(PlayerCl pl) //Отсылает координаты
        {
            byte[] reg = { 2 };
            byte[] coords = { (byte)pl.X, (byte)pl.Y };
            soc.SendTo(reg, servIP);
            Thread.Sleep(1);
            soc.SendTo(coords, servIP);
        }
        public void Send_Token(PlayerCl pl) //Отсылает класс Player в начале игры
        {
            byte[] reg = { 1 };
            byte[] coords = {(byte)pl.X, (byte)pl.Y, (byte)(pl.M)};
            
            soc.SendTo(reg, servIP);
            Thread.Sleep(5);
            soc.SendTo(coords, servIP);
            Thread.Sleep(5);
            soc.SendTo(Encoding.ASCII.GetBytes(pl.Name), servIP);
        }
        public void Send_ExitMes() //Отсылает сообщение о выходе
        {
            byte[] mes = {255};
            soc.SendTo(mes, servIP);
        }
        private byte[] Reciever()
        {
           // Console.WriteLine("Listening" + groupEP.ToString());
            byte[] bytes = listener.Receive(ref groupEP);
            Console.WriteLine("Received broadcast from {0} :\n {1}\n",
            groupEP.ToString(),
           Encoding.ASCII.GetString(bytes, 0, bytes.Length).Length);
            Console.WriteLine(groupEP.Address);
            return bytes;
        }
        public void Listen()
        {
            //file = 
            while (!isEndPack)
            {
                Listener();
            }
        }
        private void Listener()
        {
            Console.WriteLine("Waiting for broadcast");
        //    StreamWriter str = File.AppendText("C:\\Users\\Artem\\Documents\\tmp.txt");
        //    StreamWriter str = File.AppendText("C:\\Users\\Artem\\Documents\\newtesting.txt");
         //   using (str)
          //  {
          //      str.WriteLine("Waiting for broadcast");
            // }
            // str.Close();
                //System.Environment.Exit(0);
                byte[] tmp = Reciever();
                Console.WriteLine(tmp.Length);
                //    str = File.AppendText("C:\\Users\\Artem\\Documents\\tmp.txt");
                //  using (str)
              //  {
                //    str.WriteLine(tmp.Length.ToString());
                // }
                //str.Close();
                UDPListener.Map.cur_players = (int)tmp[0];
                if (UDPListener.Map.cur_players != 255)
                {
                    try
                    {
                        UDPListener.Map.ByteToMap(Reciever());
                        Console.WriteLine("Recieved Map");
                        //   str = File.AppendText("C:\\Users\\Artem\\Documents\\tmp.txt");
                        // using (str)
                        //{
                        //   str.WriteLine("Recieved Map");
                        // }
                        //str.Close();
                        UDPListener.Map.ByteToCharArr(Reciever());
                        Console.WriteLine("Recieved char arr");
                        //  str = File.AppendText("C:\\Users\\Artem\\Documents\\tmp.txt");
                        //  using (str)
                        //  {
                        //    str.WriteLine("Recieved char arr");
                        // }
                        //  str.Close();
                        UDPListener.Map.notif = UDPListener.Map.ByteToString(Reciever());
                        Console.WriteLine("Recieved notif");
                        //    str = File.AppendText("C:\\Users\\Artem\\Documents\\tmp.txt");
                        //   using (str)
                        //  {
                        //     str.WriteLine("Recieved notif");
                        // }
                        //   str.Close();
                        string[] ret = new string[UDPListener.Map.cur_players];
                        for (int i = 0; i < UDPListener.Map.cur_players; ++i)
                            ret[i] = UDPListener.Map.ByteToString(Reciever());
                        Console.WriteLine("Recieved names");
                        //  str = File.AppendText("C:\\Users\\Artem\\Documents\\tmp.txt");
                        //   using (str)
                        //  {
                        //    str.WriteLine("Recieved names");
                        //  }
                        // str.Close();
                        UDPListener.Map.players_names = ret;
                        //  str.Close();
                    }
                    catch 
                    {
                        Console.WriteLine("Bad packet. IGNORED!");
                    }
                }

                else
                    isEndPack = true;
            
        }
    }
}

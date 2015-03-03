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
    class Send
    {
        Socket s;
        public Send(Socket s)
        {
            this.s = s;
        }
        public void SendEndingMessageToAll(List<PlayerServ> pls)
        {
            for (int i = 0; i < pls.Count(); ++i)
                SendEndingMessage(pls[i].IP);
        }
        private void SendByte(IPEndPoint curip, byte b)
        {
            byte[] tmp = { b };
            s.SendTo(tmp, curip);
        }
        public void SendConnMes(IPEndPoint curip)
        {
            SendByte(curip, 0);
        }


        public void SendEndingMessage(IPEndPoint curip)
        {
            SendByte(curip, 255);
        }
        public void SendNumbOfPlayers(IPEndPoint curip, int num)
        {
            SendByte(curip, (byte)num);
        }
        public void SendAll(ref MapServ cur, List<PlayerServ> pls)
        {
            cur.cur_players = pls.Count();
            for (int i = 0; i < pls.Count(); ++i)
            {
                cur.players_names[i] = pls[i].Name;
                cur.players_signs[i] = pls[i].M;
            }
            byte[] num = { (byte)cur.cur_players };
            for (int i = 0; i < pls.Count(); ++i)
            {
                s.SendTo(num, pls[i].IP);
            }
            Thread.Sleep(3);
            for (int i = 0; i < pls.Count(); ++i)
            {
                s.SendTo(EncodingB.EncodingIntArrToByteStream(cur), pls[i].IP);
            }
            Thread.Sleep(3);
            for (int i = 0; i < pls.Count(); ++i)
            {
                s.SendTo(EncodingB.EncodingCharArrToByteStream(cur), pls[i].IP);
            }
            Thread.Sleep(3);
            for (int i = 0; i < pls.Count(); ++i)
            {
                s.SendTo(EncodingB.EncodingStringToByteStream(cur.notif), pls[i].IP);
                byte[][] names = EncodingB.EncodingArrStringToByteStream(cur);
                for (int k = 0; k < cur.cur_players; ++k)
                    s.SendTo(names[k], pls[k].IP);
            }
            Console.WriteLine("Sent to all");
        }
    }
}

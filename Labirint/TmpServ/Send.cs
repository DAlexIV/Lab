﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

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
        public void SendEndingMessage(IPEndPoint curip)
        {
            byte[] tmp = { 255 };
            s.SendTo(tmp, curip);
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
            for (int i = 0; i < pls.Count(); ++i)
            {
                s.SendTo(EncodingB.EncodingIntArrToByteStream(cur), pls[i].IP);
            }
            for (int i = 0; i < pls.Count(); ++i)
            {
                s.SendTo(EncodingB.EncodingCharArrToByteStream(cur), pls[i].IP);
            }
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

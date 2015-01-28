using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpServ
{
    class PlayerServ : GameClasses.Player
    {
        public string IP;
        public PlayerServ(string IP, int x, int y, char ch, string name)
        {
            this.IP = IP;
            this.x = x;
            this.x = y;
            this.m = ch;
            this.name = name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace TmpServ
{
    class PlayerServ : GameClasses.Player
    {
        public IPEndPoint IP;
        public PlayerServ(int x, int y, char ch, string name)
        {
            this.x = x;
            this.x = y;
            this.m = ch;
            this.name = name;
        }
    }
}

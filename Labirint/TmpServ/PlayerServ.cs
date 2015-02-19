using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace TmpServ
{
    public class PlayerServ : GameClasses.Player
    {
        public IPEndPoint IP;
        public PlayerServ(char ch, string name, ref MapServ map, int n)
        {

            gen_position(map, n + 1);
            this.m = ch;
            this.name = name;
        }
    }
}

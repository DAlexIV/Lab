using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpServ
{
    class Decoding
    {
        static internal PlayerServ BToPlayer(byte[] mes, byte[] strmes)
        {
            return new PlayerServ(mes[1], mes[0], (char)mes[2], Encoding.ASCII.GetString(strmes));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpClient
{
    class MapCl : GameClasses.Map
    {
        public void ByteToMap(byte[] mes)
        {
            for (int i = 0; i < map_height; ++i)
                for (int k = 0; k < map_width; ++k)
                    map[i, k] = mes[i * map_width + k] - 128;
        }
        public void ByteToCharArr(byte[] mes)
        {
            char[] ret = new char[cur_players];
            for (int i = 0; i < cur_players; ++i)
                ret[i] = (char)mes[i];
            players_signs = ret;
        }
        public string ByteToString (byte[] s)
        {
            return Encoding.ASCII.GetString(s);
        }
    }
}

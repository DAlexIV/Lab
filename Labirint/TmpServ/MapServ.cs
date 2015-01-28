using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpServ
{
    class MapServ : GameClasses.Map
    {
        List<GameClasses.Player> pls;
        
        public byte[] EncodingCharArrToByteStream() //Перекодировка значков игроков в byte[]
        {
            byte[] stream = new byte[max_players_num];
            for (int i = 0; i < cur_players; ++i)
                stream[i] = (byte)players_signs[i];
            return stream;
        }
        public byte[] EncodingStringToByteStream(string line)
        {
            return Encoding.ASCII.GetBytes(line);
        }
        public byte[][] EncodingArrStringToByteStream()
        {
            byte[][] streams = new byte[cur_players][];
            for (int i = 0; i < cur_players; ++i)
                streams[i] = EncodingStringToByteStream(players_names[i]);
            return streams;
        }
        public int[,] GetMap
        {
            get
            {
                return map; 
            }
            set
            {
                map = value;
            }
        }
    }
}

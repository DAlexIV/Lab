using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpServ
{
    class MapServ : GameClasses.Map
    {
        public byte[] EncodingIntArrToByteStream() //Перекодировка карты в byte[]
        {
            byte[] stream = new byte[map_height * map_width];
            for (int i = 0; i < map_height; ++i)
                for (int j = 0; j < map_width; ++j)
                    //Добавляем значениям в массиве интов 128 (так как у нас есть отрицательные числа - игроки)
                    //Чтобы все хорошо конвертилось в байт
                    stream[i * map_width + j] = (byte)(map[i,j] + 128);
            return stream;
        }
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

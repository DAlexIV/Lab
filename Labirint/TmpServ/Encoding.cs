using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpServ
{
    class EncodingB
    {
        static public byte[] EncodingIntArrToByteStream(MapServ obj) //Перекодировка карты в byte[]
        {
            byte[] stream = new byte[MapServ.map_height * MapServ.map_width];
            for (int i = 0; i < MapServ.map_height; ++i)
                for (int j = 0; j < MapServ.map_width; ++j)
                    //Добавляем значениям в массиве интов 128 (так как у нас есть отрицательные числа - игроки)
                    //Чтобы все хорошо конвертилось в байт
                    stream[i * MapServ.map_width + j] = (byte)(obj.map[i, j] + 128);
            return stream;
        }
        static public byte[] EncodingCharArrToByteStream(MapServ obj) //Перекодировка значков игроков в byte[]
        {
            byte[] stream = new byte[MapServ.max_players_num];
            for (int i = 0; i < obj.cur_players; ++i)
                stream[i] = (byte)obj.players_signs[i];
            return stream;
        }
        static public byte[] EncodingStringToByteStream(string line)
        {
            return Encoding.ASCII.GetBytes(line);
        }
        static public byte[][] EncodingArrStringToByteStream(MapServ obj)
        {
            byte[][] streams = new byte[obj.cur_players][];
            for (int i = 0; i < obj.cur_players; ++i)
                streams[i] = EncodingStringToByteStream(obj.players_names[i]);
            return streams;
        }
    }
}

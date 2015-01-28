﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpServ
{
    class Encoding
    {
        static public byte[] EncodingIntArrToByteStream(MapServ obj) //Перекодировка карты в byte[]
        {
            byte[] stream = new byte[obj.map_height * obj.map_width];
            for (int i = 0; i < map_height; ++i)
                for (int j = 0; j < map_width; ++j)
                    //Добавляем значениям в массиве интов 128 (так как у нас есть отрицательные числа - игроки)
                    //Чтобы все хорошо конвертилось в байт
                    stream[i * map_width + j] = (byte)(map[i, j] + 128);
            return stream;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MapCl : GameClasses.Map
    {
        int[,] new_map = new int[GameClasses.Map.map_height, GameClasses.Map.map_width];
        public void generateCl_map()
        {
            /* for (int i = 0; i < map.GetLength(0); i++)
                 for (int j = 0; j < map.GetLength(1); j++)
                 {
                     new_map[i, j] = map[i, j];
                     map[i, j] = 13;
                 }*/
        }
        public void ByteToInt(byte[] from)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    new_map[i, j] = from[i * map.GetLength(1) + j];
        }
        public void draw()  //метод отрисовки карты
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (new_map[i, j] != map[i, j])
                        switch (new_map[i, j])
                        {
                            case 0:
                                Console.SetCursorPosition(j, i + 2);
                                Console.ResetColor();
                                Console.Write(' ');
                                break;
                            case 1:
                                Console.SetCursorPosition(j, i + 2);
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.Write(' ');
                                break;
                            case -1:
                                Console.SetCursorPosition(j, i + 2);
                                Console.ResetColor();
                                Console.Write('#');
                                break;
                        }
                    map[i, j] = new_map[i, j];
                }

        }
        public int[,] Map
        {
            get { return new_map; }
        }
        public void ByteToMap(byte[] mes)
        {
            if (mes.Length != map_height * map_width)
                throw new Exception("Wrong packet type");
            for (int i = 0; i < map_height; ++i)
                for (int k = 0; k < map_width; ++k)
                    this.Map[i, k] = (int)(mes[i * map_width + k]) - 128;
        }
        public void ByteToCharArr(byte[] mes)
        {
            if (mes.Length != 16)
                throw new Exception("Wrong packet type");
            char[] ret = new char[cur_players];
            for (int i = 0; i < cur_players; ++i)
                ret[i] = (char)mes[i];
            players_signs = ret;
        }
        public string ByteToString(byte[] s)
        {
            return Encoding.ASCII.GetString(s);
        }
    }
}

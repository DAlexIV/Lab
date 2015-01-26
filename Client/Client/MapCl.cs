using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MapCl:GameClasses.Map
    {
        int[,] new_map = new int[GameClasses.Map.map_height, GameClasses.Map.map_width];
        public void generateCl_map()
        {
            new_map = generate_map();
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
                        if (new_map[i,j] != map[i, j])
                            switch (new_map[i, j])
                            {
                                case 0:
                                    Console.SetCursorPosition(j, i);
                                    Console.ResetColor();
                                    Console.Write(' ');
                                    break;
                                case 1:
                                    Console.SetCursorPosition(j, i);
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.Write(' ');
                                    break;
                                case -1:
                                    Console.SetCursorPosition(j, i);
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
    }
}

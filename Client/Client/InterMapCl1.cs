using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class InterMapCl1
    {
     public static void drawMap(MapCl map)
        {
            for (int i = 0; i < map.map.GetLength(0); i++)
                for (int j = 0; j < map.map.GetLength(1); j++)
                {
                    if (map.Map[i, j] != map.map[i, j])
                        switch (map.Map[i, j])
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
                    map.map[i, j] = map.Map[i, j];
                }
        }
        public static void control(MapCl map,PlayerCl player)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.S:
                    if (map.Map[player.y + 1, player.x] == 0)
                    {
                        map.Map[player.y, player.x] = 0;
                        player.y++;
                    }
                    break;
                case ConsoleKey.A:
                    if (map.Map[player.y, player.x - 1] == 0)
                    {
                        map.Map[player.y, player.x] = 0;
                        player.x--;
                    }
                    break;
                case ConsoleKey.W:
                    if (map.Map[player.y - 1, player.x] == 0)
                    {
                        map.Map[player.y, player.x] = 0;
                        player.y--;
                    }
                    break;
                case ConsoleKey.D:
                    if (map.Map[player.y, player.x + 1] == 0)
                    {
                        map.Map[player.y, player.x] = 0;
                        player.x++;
                    }
                    break;
                case ConsoleKey.Q:
                    Client.GenInterCl.state = 0;
                    Console.Clear();
                    break;
            }
            map.Map[player.y, player.x] = -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
                                if ((i == 0) || (i == map.Map.GetLength(0) - 1)
                                    || (j == 0) || (j == map.Map.GetLength(1) - 1))
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                else
                                    Console.BackgroundColor = ConsoleColor.Gray;
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
        public static void control(MapCl map,PlayerCl player, Netw cn)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.S:
                    if (map.Map[player.Y + 1, player.X] != 1)
                    {
                        player.Y++;
                    }
                    break;
                case ConsoleKey.A:
                    if (map.Map[player.Y, player.X - 1] !=1)
                    {
                        player.X--;
                    }
                    break;
                case ConsoleKey.W:
                    if (map.Map[player.Y - 1, player.X] != 1)
                    {
                        player.Y--;
                    }
                    break;
                case ConsoleKey.D:
                    if (map.Map[player.Y, player.X + 1] != 1)
                    {
                        player.X++;
                    }
                    break;
                case ConsoleKey.Q:
                    Console.ResetColor();
                    Client.GenInterCl.state = 0;
                    Console.Clear();
                    for (int i = 0; i < map.map.GetLength(0); i++)
                        for (int j = 0; j < map.map.GetLength(1); j++)
                            map.map[i, j] = 0;
                        break;
            }
            
            cn.Send_Coords(player);
            Thread.Sleep(30);
        }
    }
}

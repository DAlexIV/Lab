using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class PlayerCl : GameClasses.Player
    {
        public PlayerCl()
        {
            x = 0;
            y = 0;
            m = '#';
        }
        public void gen_position(Client.MapCl map)
        {
            Random rand = new Random();
            int k = 0;
            int[,] matr = new int[2, map.Map.Length];
            for (int i = 0; i < map.Map.GetLength(0); i++)
                for (int j = 0; j < map.Map.GetLength(0); j++)
                    if (map.Map[i, j] == 0)
                    {
                        matr[0, k] = i;
                        matr[1, k] = j;
                        k++;
                    }
            int p = rand.Next(k);
            y = matr[0, p];
            x = matr[1, p];
            map.Map[y, x] = -1;
        }
        public void controls(MapCl map)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.S:
                    if (map.Map[y + 1, x] == 0)
                    {
                        map.Map[y, x] = 0;
                        y++;
                    }
                    break;
                case ConsoleKey.A:
                    if (map.Map[y, x - 1] == 0)
                    {
                        map.Map[y, x] = 0;
                        x--;
                    }
                    break;
                case ConsoleKey.W:
                    if (map.Map[y - 1, x] == 0)
                    {
                        map.Map[y, x] = 0;
                        y--;
                    }
                    break;
                case ConsoleKey.D:
                    if (map.Map[y, x + 1] == 0)
                    {
                        map.Map[y, x] = 0;
                        x++;
                    }
                    break;
                case ConsoleKey.Q:
                    Client.GenInterCl.state = 0;
                    Netw.Send_ExitMes();
                    break;
            }
            map.Map[y, x] = -1;
        }
    }
}

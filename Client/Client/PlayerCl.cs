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
                for (int j = 0; j < map.Map.GetLength(1); j++)
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
    }
}

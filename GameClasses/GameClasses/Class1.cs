using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses
{
    
    public class Player
    {
        protected int x;
        protected int y;
        protected char m;
        protected string name;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (value >= 0 && value < Map.map_width)
                    x = value;
                else 
                    throw new Exception("Player overrunning on x");
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value >= 0 && value < Map.map_height)
                    y = value;
                else 
                    throw new Exception("Player overrunning on y");
            }
        }
        public char M
        {
            get
            {
                return m;
            }
            set
            {
                if (value >= 21 && value <=126)
                    x = value;
                else 
                    throw new Exception("Player sum is false");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length <= 10)
                    name = value;
                throw new Exception("Player name is too long");
            }
        }

    }
    public class Map
    {
        static Random rand = new Random(); 
        protected static char defch = '#';
        public const int map_width = 20;
        public const int map_height = 10;
        public const int max_players_num = 16;

        public int cur_players;

        public int[,] map = new int[map_height, map_width];

        // Игроки имеют номера с -1 до -16 в карте
        public char[] players_signs = new char[max_players_num];
        public string[] players_names = new string[max_players_num]; //Никнеймы игроков(чтобы показываться в где-нибудь)

        //Строка для уведомлений игроков, тоже должна показываться где-нибудь в течении какого-нибудь времени
        //В идеале это может быть даже некоторый набор строк, то есть будут показываться последние несколько
        //В случае отсутствия каких-либо действий равна null
        public string notif; 
        public int[,] generate_map()
        {
            Random gen = new Random();
            int[,] m = new int[map_height, map_width];
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    if ((i == 0) || (i == m.GetLength(0)-1) || (j == 0) || (j == m.GetLength(1)-1))
                        m[i, j] = 1;
                    else
                    { 
                    m[i, j] = gen.Next(3);
                    if (m[i, j] == 0)
                        m[i, j] = 1;
                    else
                        m[i, j] = 0;
            }
                return m;
        }
        public int[,] GetMap
        {
            get
            {
                return map;
            }
            set
            {
                if (map.GetLength(0) == Map.map_height && map.GetLength(1) == Map.map_width)
                    map = value;
                else
                    throw new Exception("Wrong map size!");
            }
        }
        public int this[int i, int j]
        {
            get
            {
                return map[i, j];
            }
            set
            {
                if (value < 2)
                    map[i, j] = value;
                else
                    throw new Exception("Wrong map value");
            }
        }
    }
}
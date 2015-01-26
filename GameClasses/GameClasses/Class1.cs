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
    }
    public class Map
    {
        static Random rand = new Random(); 
        protected static char defch = '#';
        public const int map_width = 50;
        public const int map_height = 20;
        protected const int max_players_num = 16;

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
            int[,] m = new int[map_height, map_width];
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    m[i, j] = rand.Next(2);
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
                map = value;
            }
        }
    }
}
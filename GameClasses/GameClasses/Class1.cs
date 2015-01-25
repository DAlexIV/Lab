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
        protected int[,] map = new int[20, 50];

        // Игроки имеют номера с -1 до -16 в карте
        protected char[] players = new char[16];
       // protected string[] players = new string[16]; //Никнеймы игроков(чтобы показываться в где-нибудь)

        //Строка для уведомлений игроков, тоже должна показываться где-нибудь в течении какого-нибудь времени
        //В идеале это может быть даже некоторый набор строк, то есть будут показываться последние несколько
        //В случае отсутствия каких-либо действий равна null
        protected string notif; 
        public int[,] generate_map()
        {
            int[,] m=new int[20,50];
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    m[i, j] = rand.Next(2);
            return m;
        }
    }
}

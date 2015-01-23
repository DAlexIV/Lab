using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses
{
    
    public class Player
    {
        int x;
        int y;
        char m;
        string name;
    }
    public class Map
    {
        static char defch = '#';
        int[,] map = new int[30, 60];

        // Игроки имеют номера с -1 до -16 в карте
        char[] players = new char[16];
        string[] players = new string[16]; //Никнеймы игроков(чтобы показываться в где-нибудь)

        //Строка для уведомлений игроков, тоже должна показываться где-нибудь в течении какого-нибудь времени
        //В идеале это может быть даже некоторый набор строк, то есть будут показываться последние несколько
        //В случае отсутствия каких-либо действий равна null
        string notif; 
    }
}

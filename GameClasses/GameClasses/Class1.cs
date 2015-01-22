using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses
{
    static char = '#';
    public class Player
    {
        int x;
        int y;
        char m;
        string name;
    }
    public class Map
    {
        int[,] map = new int[30, 60];
        // Игроки имеют номера с -1 до -16 в карте
        char[] players = new char[16];
    }
}

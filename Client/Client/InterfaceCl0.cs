using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class InterfaceCl0
    {
        static public bool ne = false;
        static public int state = 0;
        static char[,] map = new char[3, 50];
        static char[,] new_map = new char[3, 50];
        static public void init()
        {
            map = new char[3, 50];
            string s = "Generate map";
            for (int i = 0; i < s.Length; i++)
                new_map[0, 2 + i] = s[i];
            s = "To Be Continued..";
            for (int i = 0; i < s.Length; i++)
                new_map[2, 2 + i] = s[i];
            new_map[state, 0] = '-';
            new_map[state, 1] = '>';
        }
        /*
        static public void draw(MapCl mp)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    mp[i, j] = 0;
                Console.ResetColor();
            if (ne == false)
                init();
            for (int i = 0; i < mp.map.GetLength(0); i++)
                    for (int j = 0; j < mp.map.GetLength(1); j++)
                    {
                        if (mp.Map[i, j] != mp.map[i, j])
                            if (mp.Map[i, j] < 0 && mp.Map[i, j] > -16)
                            {
                                Console.SetCursorPosition(j, i);
                                Console.ResetColor();
                                Console.Write(mp.players_signs[-mp.Map[i, j] - 1]);
                            }
                            else
                            {
                                switch (mp.Map[i, j])
                                {
                                    case 0:
                                        Console.SetCursorPosition(j, i);
                                        Console.ResetColor();
                                        Console.Write(' ');
                                        break;
                                    case 1:
                                        Console.SetCursorPosition(j, i);
                                        if ((i == 0) || (i == mp.Map.GetLength(0) - 1)
                                            || (j == 0) || (j == mp.Map.GetLength(1) - 1))
                                            Console.BackgroundColor = ConsoleColor.DarkGray;
                                        else
                                            Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.Write(' ');
                                        break;
                                    default:
                                        throw new Exception("BAD VALUE IN MAP");
                                }
                            }
                        mp.map[i, j] = mp.Map[i, j];
                    }
        }
         */
        static public void controls()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.S:
                    if (state != 2)
                    {
                        new_map[state, 0] = ' ';
                        new_map[state, 1] = ' ';
                        state += 2; ;
                    }
                    break;
                case ConsoleKey.W:
                    if (state != 0)
                    {
                        new_map[state, 0] = ' ';
                        new_map[state, 1] = ' ';
                        state -= 2; ;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (state == 0)
                    {
                        GenInterCl.state = 1;
                        map = new char[3, 50];
                        Console.ResetColor();
                        Console.Clear();
                    }
                    break;
            }
            new_map[state, 0] = '-';
            new_map[state, 1] = '>';
        }
    }
}

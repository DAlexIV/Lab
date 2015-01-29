﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class InterfaceCl0
    {
        static public bool ne=false;
        static public int state=0;
        static char[,] map = new char[3, 50];
        static char[,] new_map = new char[3, 50];
     static public void init()
        {
            map = new char[3, 50]; 
         string s="Generate map";
         for (int i = 0; i < s.Length; i++)
             new_map[0, 2 + i] = s[i];
         s = "To Be Continued..";
         for (int i = 0; i < s.Length; i++)
             new_map[2, 2 + i] = s[i];
         new_map[state,0]='-';
         new_map[state, 1] = '>';
        }
     static public void draw()
        {
            Console.ResetColor();
            if (ne == false)
                init();
         for (int i = 0; i < map.GetLength(0); i++)
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (new_map[i,j] != map[i, j])
                        {
                            Console.SetCursorPosition(j, i);
                            Console.Write(new_map[i, j]);
                            map[i, j] = new_map[i, j];
                        }
                    }
        }
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
                     Console.Clear();
                 }
                 break;
         }
         new_map[state, 0] = '-';
         new_map[state, 1] = '>';
     }
    }
}
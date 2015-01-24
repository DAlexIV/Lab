using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class PlayerCl:GameClasses.Player
    {
     public PlayerCl()
        {
            x = 0;
            y = 0;
            m = '#';
        }
        public void controls(MapCl map)
        {
         switch(Console.ReadKey(true).KeyChar)
         {
             case 's':
                 //if (map.Map[y++,x]==0)
                 map.Map[y, x] = 0;
                 y ++;
                 
                 break;
             case 'a':
                 //if (map.Map[y++,x]==0)
                 map.Map[y, x] = 0;
                 x--;

                 break;
             case 'w':
                 //if (map.Map[y++,x]==0)
                 map.Map[y, x] = 0;
                 y--;

                 break;
             case 'd':
                 //if (map.Map[y++,x]==0)
                 map.Map[y, x] = 0;
                 x++;

                 break;
         }
         map.Map[y, x] = -1;
        }
    }
}

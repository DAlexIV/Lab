using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class GenInterCl
    {
        public static int state = 0;
        public static void controls()
        {
            switch (state)
            {
                case 0:
                    Client.InterfaceCl0.controls();
                    break;
                case 1:
                    UDPListener.Map.generateCl_map();
                    UDPListener.Player.gen_position(UDPListener.Map);
                    state = 2;
                    break;
                case 2:
                    Client.InterMapCl1.control(UDPListener.Map, UDPListener.Player, UDPListener.cur_netw);
                    break;
            }
        }
        public static void redraw()
        {

         switch (state)
         {
             case 0:
                 Client.InterfaceCl0.draw();
                 break;
             case 1:
                 UDPListener.Map.generateCl_map();
              //   UDPListener.Player.gen_position(UDPListener.Map);
                 state = 2;
                 Client.InterMapCl1.drawMap(UDPListener.Map);
                 break;
             case 2:
                 Client.InterMapCl1.drawMap(UDPListener.Map);
                 break;
         }
        }
    }
}

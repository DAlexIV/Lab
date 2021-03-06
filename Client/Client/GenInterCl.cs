﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class GenInterCl
    {
        public static event EventHandler<MapArgs> MapChanged;
        public static int state = 0;
        static GenInterCl()
        {
            MapChanged += InterMapCl1.drawMap;
        }
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
        public static void redraw(MapCl mp)
        {
         switch (state)
         {
             case 0:
                 MapArgs args = new MapArgs(UDPListener.Map);
                 MapChanged(null, args);
                 break;
         }
        }
    }
}

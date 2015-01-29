using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPListener
{
    public static Client.MapCl Map=new Client.MapCl();
    public static Client.PlayerCl Player=new Client.PlayerCl();
    private const int listenPort = 11000;

    public static int Main()
    {
        Console.WindowHeight = 35;
        Console.WindowWidth = 100;
        Console.CursorVisible = false;
        Client.InterfaceCl0.init();
        while (true)
        {
            Client.GenInterCl.redraw();
            Client.GenInterCl.controls();
        }
        Console.ReadKey();
        return 0;
    }
}
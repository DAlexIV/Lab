using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPListener
{
    private const int listenPort = 11000;

    private static void StartListener()
    {
        bool done = false;

        
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 0);
        UdpClient listener = new UdpClient(listenPort);

        try
        {
            while (!done)       
            {
                Console.WriteLine("Waiting for broadcast");
                byte[] bytes = listener.Receive(ref groupEP);
                
                Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                    groupEP.ToString(),
                    Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            listener.Close();
        }
    }

    public static int Main()
    {
        Console.WindowHeight = 35;
        Console.WindowWidth = 100;
        int state = 0; 
        Console.CursorVisible = false;
        Client.MapCl test = new Client.MapCl();
        Client.PlayerCl pl = new Client.PlayerCl();
        //StartListener();
        Client.Interface.init();
        while(true)
        {
            switch(Client.Interface.state)
            {
                case 0:
            Client.Interface.draw();
            Client.Interface.controls(ref Client.Interface.state);
            break;
                case 1:
            test.generateCl_map();
            pl.gen_position(test);
                    while(true)
        {          
            test.draw();
            pl.controls(test);
        }
            break;
            }
        }
        /*Client.MapCl test = new Client.MapCl();
        Client.PlayerCl pl = new Client.PlayerCl();
        test.generateCl_map();
        while(true)
        {          
            test.draw();
            pl.controls(test);
        }*/
        Console.ReadKey();
        return 0;
    }
}
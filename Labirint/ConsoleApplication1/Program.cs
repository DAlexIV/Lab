using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TmpServ.MapServ testi = new TmpServ.MapServ();
            Random rand = new Random();
            int width = 20,height=10;
            byte[] mas = new byte[width * height + 2];
            mas[0]=(byte)width;
            mas[1]=(byte)height;
            string s = @"C:\\Users\\Artem\\Documents\\testingmap.lab";
            FileInfo file = new FileInfo(s);
            FileStream wr = file.OpenWrite();
            for (int i = 2; i < width * height + 2; i++)                   
                    mas[i] =(byte)rand.Next(3);
            wr.Write(mas, 0, mas.Length);
            Console.WriteLine(mas[0]+" "+mas[1]);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    if (!(((i==0)&&(j==0))||((i==0)&&(j==1))))
                    Console.Write(mas[i * width + j] + " ");
                Console.WriteLine();
                }
            Console.WriteLine();
            wr.Close();
            testi.GenerateMapFromFile(s);
            for (int i = 0; i < testi.GetMap.GetLength(0); i++)
            {
                for (int j = 0; j < testi.GetMap.GetLength(1); j++)
                    Console.Write(testi[i, j] + " ");
                Console.WriteLine();
            }
                    Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a =new int[1];
            a[0] = 5;
            Array.Resize(ref a, 2);
            a[1] = -1;
            
            
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");
                Console.ReadKey();
        }
    }
}

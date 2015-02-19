using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TmpServ
{
    public class MapServ : GameClasses.Map
    {
        private class Spec
        {
            public int x;
            public int y;
            public Spec(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        static private bool chec(int x, int y, int[,] matr)
        {
            if ((x >= 0) && (x < matr.GetLength(1)) && (y >= 0) && (y < matr.GetLength(0)))
                return true;
            else
                return false;
        }
        static private bool srav(List<Spec> ob, int x, int y)
        {
            for (int i = 0; i < ob.Count; i++)
                if ((ob[i].x == x) && (ob[i].y == y))
                    return true;
            return false;
        }
        public void GenerateMapNow()
        {
          Random rand = new Random();
            int width = map_width-2;
            int height = map_height-2;
            int[,] matrnew = new int[height + 2, width + 2];
            int[,] matr = new int[height, width];
            List<Spec> masspec = new List<Spec>();
            for (int i = 0; i < matr.GetLength(0); i++)
                for (int j = 0; j < matr.GetLength(1); j++)
                    matr[i, j] = 1;
            int x = rand.Next(width);
            int y = rand.Next(height);
            matr[y, x] = 0;
            int ad, dop, vsi = 0;
            while ((masspec.Count != 0) || (vsi == 0))
            {
                vsi = 1;
                if ((chec(x - 1, y, matr)) && (matr[y, x - 1] == 1))
                {
                    if (srav(masspec, x - 1, y) == false)
                    {
                        masspec.Add(new Spec(x - 1, y));
                    }

                }
                if ((chec(x + 1, y, matr)) && (matr[y, x + 1] == 1))
                {
                    if (srav(masspec, x + 1, y) == false)
                    {
                        masspec.Add(new Spec(x + 1, y));
                         
                    }
                }
                if ((chec(x, y - 1, matr)) && (matr[y - 1, x] == 1))
                {
                    if (srav(masspec, x, y - 1) == false)
                    {
                        masspec.Add(new Spec(x, y - 1));
                         
                    }
                }
                if ((chec(x, y + 1, matr)) && (matr[y + 1, x] == 1))
                {
                    if (srav(masspec, x, y + 1) == false)
                    {
                        masspec.Add(new Spec(x, y + 1));
                         
                    }
                }
                dop = 0;
                int s;
                while (dop == 0)
                {
                    s = 0;
                    ad = rand.Next(masspec.Count);
                    if ((chec(masspec[ad].x - 1, masspec[ad].y, matr)) && (matr[masspec[ad].y, masspec[ad].x - 1] == 0))
                        s++;
                    if ((chec(masspec[ad].x + 1, masspec[ad].y, matr)) && (matr[masspec[ad].y, masspec[ad].x + 1] == 0))
                        s++;
                    if ((chec(masspec[ad].x, masspec[ad].y - 1, matr)) && (matr[masspec[ad].y - 1, masspec[ad].x] == 0))
                        s++;
                    if ((chec(masspec[ad].x, masspec[ad].y + 1, matr)) && (matr[masspec[ad].y + 1, masspec[ad].x] == 0))
                        s++;
                    if (s == 1)
                    {
                        x = masspec[ad].x;
                        y = masspec[ad].y;
                        dop = 1;
                        matr[masspec[ad].y, masspec[ad].x] = 0;
                        masspec.Remove(masspec[ad]);                       
                    }
                    else
                    {
                        masspec.Remove(masspec[ad]);
                        if (masspec.Count == 0)
                            dop = 1;
                    }
                }
            }
            for (int i = 0; i < map_height; i++)
                for (int j = 0; j < map_width; j++)
                {
                    if ((i == 0) || (i == map_height - 1) || (j == 0) || (j == map_width - 1))
                        map[i, j] = 1;
                    else
                        map[i, j] = matr[i - 1, j - 1];
                }  
        }
        public void GenerateMapFromFile(string name)
        {
            Exception filefail = new Exception("Loading of file is failed");
            int width, height;
            if (File.Exists(name) == true)
            {
                FileInfo file = new FileInfo(name);
                FileStream re = file.OpenRead();
                width = re.ReadByte();
                height = re.ReadByte();
                this.map = new int[height, width];
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                        this.map[i, j] = re.ReadByte() - 128 ;
            }
            else
                throw filefail;
        }
    }
}

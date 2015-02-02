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
        
        static void GenerateMapNow(int step)
        {
            
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
                        this.map[i, j] = re.ReadByte();
            }
            else
                throw filefail;
        }
    }
}

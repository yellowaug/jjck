using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES128pro
{
    public class ReadFile
    {
        public BinaryReader keyFile { get; private set; }
        public byte[] keyBytes { get; private set; }
        private void readFileKey(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            Console.WriteLine(file);
            try
            {
               
                byte[] by = new byte[file.Length];
                this.keyFile = new BinaryReader(file);
                keyFile.Read(by, 0, by.Length); //<<------这个写法似乎不能读取太大文件
                this.keyBytes = by;
                //string str = Encoding.UTF8.GetString(by);
                //Console.WriteLine(str);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
                      
        }
        public ReadFile(string path)
        {
            this.readFileKey(path);
        }


    }
}

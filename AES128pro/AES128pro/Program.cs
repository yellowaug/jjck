using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES128pro
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] byteKey = new ReadFile(@"F:\s2programe\230.php").keyBytes; //加载文件
            byte[] byteFile = new ReadFile(@"F:\s2programe\230.ts").keyBytes; //加载密钥
            new AESFile(byteKey, byteFile, @"F:\s2programe\231.mp4");  //解密
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AES128pro
{
    public class AESFile
    {
        private void AESDecrypt(byte[] key,byte[] filecontext,string path)
        {
            double total = 0;
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = key;
            //rDel.Mode = CipherMode.ECB;
            //rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(filecontext, 0, filecontext.Length);
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter binaryWriter = new BinaryWriter(file);
            int size = (filecontext.Length / 1024) / 1024;
            for (int i = 0; i < resultArray.Length; i++)
            {
                total = (i / 1024) / 1024;
                binaryWriter.Write(resultArray[i]);
                Console.WriteLine("文件大小"+size+"MB"+"进度"+total+"MB");
                //Console.WriteLine(i);
                //Console.WriteLine(total);
                
            }
            //foreach (var contextitem in resultArray)
            //{               
            //    total = (resultArray.Length-1)/100;

            //    binaryWriter.Write(contextitem);
            //    Console.WriteLine("正在解密"+total);
            //}
            
        }
        public AESFile(byte[] key, byte[] filecontext,string path)
        {
            this.AESDecrypt(key, filecontext,path);
            Console.WriteLine("解密完成");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace socketServer
{

    public delegate void FileAction(byte[] recvFileCon, string sockfilePath);
    public delegate string SFolderAction(string directory);

    public class SocketFileAction
    {
        /// <summary>
        /// 将接收到的数据包写入文件
        /// </summary>
        /// <param name="recvFileCon">接收到的数据包</param>
        /// <param name="sockfilePath">写入的文件路径</param>
        public void WriteToFile(byte[] recvFileCon, string sockfilePath)
        {
            var fs = File.Open(sockfilePath, FileMode.Append);
            //var fs = File.OpenWrite(@"e:\111sock.xps");
            fs.Write(recvFileCon, 0, recvFileCon.Length);
            fs.Close();
        }
    }
    public class SocketFolderAction
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="directory">文件夹路径</param>
        /// <returns></returns>
        public string CreateFolder(string directory)
        {
            string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            Console.WriteLine($"获取到的日期是{getDate}");
            string folderName = String.Format($"{getDate},socket");
            Console.WriteLine($"生成的文件名为{folderName}");
            string path = Path.Combine(directory, folderName);
            var existsFolder = Directory.Exists(path);
            if (existsFolder!=true)
            {
                Directory.CreateDirectory(path);
                return path;
            }
            else
            {
                return Directory.GetDirectoryRoot(path);
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folderDirectory">文件夹路径</param>
        public void DeleteFolder(string folderDirectory)
        {
            try
            {
                Directory.Delete(folderDirectory, true);
                Console.WriteLine($"{folderDirectory}删除成功");
            }
            catch (Exception ioe)
            {
                Console.WriteLine("删除失败:\n{0}",ioe);               
            }
        }
    }

}


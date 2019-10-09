using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FTPClient
{
    public class FolderAction
    {
        public string CreateFolder(string directory)
        {
            string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            Console.WriteLine($"获取到的日期是{getDate}");
            string folderName = String.Format($"{getDate}ftp");
            Console.WriteLine($"生成的文件名为{folderName}");
            string path = Path.Combine(directory, folderName);
            var existsFolder = Directory.Exists(path);
            if (existsFolder != true)
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("文件夹创建成功");
                return path;
            }
            else
            {
                Console.WriteLine("文件夹已存在，不用创建直接返回路径{0}", path);
                return path;
            }
        }
        public void DeleteFolder(string folderDirectory)
        {
            try
            {
                Directory.Delete(folderDirectory, true);
                Console.WriteLine($"{folderDirectory}删除成功");
            }
            catch (Exception ioe)
            {
                Console.WriteLine("删除失败:\n{0}", ioe);
            }
        }
    }
}

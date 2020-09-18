using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateFolder
{
    public class CreateFolder
    {
        public string CreateFolderAction(string directory)
        {
            string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            Console.WriteLine($"获取到的日期是{getDate}");
            string folderName = String.Format($"yh{getDate}");
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
    }
}

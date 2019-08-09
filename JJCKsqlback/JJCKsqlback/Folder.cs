using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JJCKsqlback
{
    public interface ICurrentPath
    {
        void SetPath(string direPath);
    }
    public interface IGetFolderInfo
    {
        DirectoryInfo[] GetFolderInfo();
    }
    public interface ICreateFolder
    {
        string Create();
    }
    public interface IDeletFolder
    {
        void Delete();
    }
    /// <summary>
    /// 这个类里生成的参数应该都要传入属性里，这样方便其他类调用，明天在公司实现一下
    /// </summary>
    class Folder : ICreateFolder,IDeletFolder,IGetFolderInfo,ICurrentPath
    {
        
        string ICreateFolder.Create()
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string folderName= @"c:\testFolder";
            string timeFull = dt.ToString("yyyyMMddTHHmmss");
            string pathStr = Path.Combine(folderName, timeFull);
            try
            {
                Directory.CreateDirectory(pathStr);
                Console.WriteLine($"文件创建成功：{pathStr}");
                return pathStr;
            }
            catch (IOException ioe)
            {
                Console.WriteLine($"文件创建失败,返回的异常是：\n{ioe.Message}");
                return ioe.Message;
            }   
             
        }
        void IDeletFolder.Delete()
        {

        }

        DirectoryInfo[] IGetFolderInfo.GetFolderInfo()
        {
            DirectoryInfo diinfo = new DirectoryInfo(@"c:\testFolder");
            foreach (var filelist in diinfo.GetDirectories())
            {
                Console.WriteLine("当前文件夹下的子文件夹：{0}",filelist.Name);
            }
            return diinfo.GetDirectories();

        }

        void ICurrentPath.SetPath(string direPath)
        {

            try
            {
                //Set the current directory.
                Directory.SetCurrentDirectory(direPath);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The specified directory does not exist. {0}", e);
            }
            // Print to console the results.
            Console.WriteLine("根目录路径: {0}", Directory.GetDirectoryRoot(direPath));
            Console.WriteLine("当前文件路径: {0}", Directory.GetCurrentDirectory());
        }
    }
    
}

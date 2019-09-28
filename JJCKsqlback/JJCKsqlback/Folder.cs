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
        List<Dirinfo> GetFolderInfo(string folderpath);
    }
    public interface ICreateFolder
    {
        string Create(string filePath);
    }
    public interface IDeletFolder
    {
        void Delete(string dirpath);
    }
    public interface IEumFiles
    {
        IEnumerable<FileInfo> EumFile(Dirinfo dirinfo);
    }
    /// <summary>
    /// 这个类里生成的参数应该都要传入属性里，这样方便其他类调用，明天在公司实现一下
    /// </summary>
    public class Folder : ICreateFolder,IDeletFolder,IGetFolderInfo,ICurrentPath,IEumFiles
    {
        
        string ICreateFolder.Create(string filePath)
        {
            DateTime dt = DateTime.Now;
            //dt = DateTime.Now;
            string folderName= filePath;
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
        void IDeletFolder.Delete(string dirpath)
        {
            try
            {
                if (Directory.Exists(dirpath))
                {
                    Directory.Delete(dirpath, true);
                    Console.WriteLine($"文件夹以及子文件{dirpath}删除成功");
                }                
            }
            catch (ArgumentException argexc)
            {

                Console.WriteLine(argexc);
            }
            catch (DirectoryNotFoundException direxcp)
            {
                Console.WriteLine(direxcp);
            }
            catch (IOException ioexcp)
            {
                Console.WriteLine(ioexcp);
            }
            catch (UnauthorizedAccessException unauexcp)
            {
                Console.WriteLine(unauexcp);
            }

        }

        IEnumerable<FileInfo> IEumFiles.EumFile(Dirinfo dirinfo)
        {
            DirectoryInfo directory = new DirectoryInfo(dirinfo.FullPath);
            var fileinfoEnu = directory.EnumerateFiles();
            if (fileinfoEnu!=null)
            {
                foreach (var file in fileinfoEnu)
                {
                    Console.WriteLine(file.FullName);
                }
            }
            else
            {
                Console.WriteLine("文件夹为空");
            }
            return fileinfoEnu;
        }

        List<Dirinfo> IGetFolderInfo.GetFolderInfo(string folderpath)
        {
            List<Dirinfo> dirinfolist = new List<Dirinfo>();
            DirectoryInfo diinfo = new DirectoryInfo(folderpath);
            foreach (var filelist in diinfo.GetDirectories())
            {
                Dirinfo dirinfo = new Dirinfo();
                dirinfo.FolderName = filelist.Name;
                dirinfo.RootPath = folderpath;
                dirinfo.FullPath = Path.Combine(folderpath, filelist.Name);
                dirinfolist.Add(dirinfo);
                Console.WriteLine("当前文件夹下的子文件夹：{0}",filelist.Name);
            }
            return dirinfolist;
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

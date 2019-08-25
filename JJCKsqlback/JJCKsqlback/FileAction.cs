using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JJCKsqlback
{
   
    class FileAction
    {
        private LogFile logfile = new LogFile();
        public void FileShell()
        {
            
            ///<summary>
            ///获取子文件夹列表,并删除文件夹以及文件
            ///</summary>
            IGetFolderInfo folderInfo = new Folder();
            //IEumFiles eumFiles = new Folder();
            IDeletFolder deletFolder = new Folder();
            string filepath = @"E:\DataBase_AutoBak"; //这个路径记当换机子的时候记得修改
            var floderobjinfo = folderInfo.GetFolderInfo(filepath);
            for (int i = 0; i < floderobjinfo.Count - 1; i++)
            {
                deletFolder.Delete(floderobjinfo[i].FullPath);
                logfile.CreateLogFile("\n============================================");
                logfile.CreateLogFile($"\n{floderobjinfo[i].FullPath}已成功删除");
                logfile.CreateLogFile("\n============================================");

            }
        }
        /// <summary>
        /// 这个是删除网站目录图片缓存文件的方法
        /// </summary>
        public void DeleteFileShell()
        {
            IGetFolderInfo getFolder = new Folder();
            IDeletFolder deletFolder = new Folder();
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string webrootpath = @"D:\WebSrv\jj.gxjyy.com";//这是正式生产用的地址
            string folderpath = Path.Combine(webrootpath, "Annex");
            string folderPadpath = Path.Combine(webrootpath, "Pad", "Annex");
            string folderPhonepath = Path.Combine(webrootpath, "Phone", "Annex");
            var folderList=getFolder.GetFolderInfo(folderpath);
            var folderPadList = getFolder.GetFolderInfo(folderPadpath);
            var folderPhoneList = getFolder.GetFolderInfo(folderPhonepath);
            Console.WriteLine($"生成的目录{folderpath}");
            if (folderList.Count==0)
            {
                Console.WriteLine($"{folderpath}目录下为空");
                logfile.CreateLogFile($"{folderpath}目录下为空\n", "AutoDeletelog.txt");
            }
            else
            {
                foreach (var folder in folderList)
                {
                    Console.WriteLine(folder.FullPath);
                    deletFolder.Delete(folder.FullPath);
                    logfile.CreateLogFile($"文件{folder.FullPath}删除成功\t删除时间{time}\n","AutoDeletelog.txt");
                }
            }
            if (folderPadList.Count==0)
            {
                Console.WriteLine($"{folderPadpath}目录下为空");
                logfile.CreateLogFile($"{folderPadpath}目录下为空\n", "AutoDeletelog.txt");
            }
            else
            {
                foreach (var folder in folderPadList)
                {
                    Console.WriteLine(folder.FullPath);
                    deletFolder.Delete(folder.FullPath);
                    logfile.CreateLogFile($"文件{folder.FullPath}删除成功\t删除时间{time}\n", "AutoDeletelog.txt");
                }
            }
            if (folderPhoneList.Count == 0)
            {
                Console.WriteLine($"{folderPhonepath}目录下为空");
                logfile.CreateLogFile($"{folderPhonepath}目录下为空\n", "AutoDeletelog.txt");
            }
            else
            {
                foreach (var folder in folderPhoneList)
                {
                    Console.WriteLine(folder.FullPath);
                    deletFolder.Delete(folder.FullPath);
                    logfile.CreateLogFile($"文件{folder.FullPath}删除成功\t删除时间{time}\n", "AutoDeletelog.txt");
                }
            }

        }
        public void TestShell()
        {
            Console.WriteLine("测试方法运行成功");
        }

    }
}

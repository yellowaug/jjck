using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 循环任务程序部分
            //循环定时任务
            TimingAction timingAction = new TimingAction();
            RunAction runAction = new RunAction();
            Action action = runAction.Run;
            while (true)
            {
                timingAction.TimeCompare(action, 6);
            }
            #endregion

            #region 及时运行的程序部分
            ////FTP操作
            //FolderAction folderAction = new FolderAction();
            //var fpath = folderAction.CreateFolder(@"E:\FTPRever");
            //Console.WriteLine("文件夹的路径是{0}", fpath);
            //string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            //string ftpFolder = string.Format($"{getDate}T030000");
            //string[] dbList = {"JJ_Communication", "JJ_Sale", "JJ_System" };
            ////string[] dbList = { "aspnetdb", "JJlinshi" };
            //string uname = "autoDL";
            //string pwd = "123";
            //foreach (var item in dbList)
            //{
            //    string ftppath = string.Format(@"ftp://10.12.2.9/{0}/{1}", ftpFolder, item);
            //    Console.WriteLine("生成的路径是");
            //    FtpAction ftp = new FtpAction(ftppath, uname, pwd);
            //    string localpath = string.Format(@"{0}\{1}", fpath, item);
            //    ftp.FtpDownLocal(localpath);
            //}
            ////数据库操作
            //IConnectionDb connectionDb = new SQLBackupAction();
            //IRevertDbAction revertDb = new SQLBackupAction();
            //var connet = connectionDb.InitConnection();
            //var dbinfos = revertDb.GetDbrevert(connet, fpath, dbList);
            //foreach (var itemdbinfo in dbinfos)
            //{
            //    revertDb.AutoRevert(itemdbinfo, connet);
            //}
            //connectionDb.Closeconnection(connet);
            //folderAction.DeleteFolder(fpath);
            //Console.Read();
            #endregion

        }
    }
}

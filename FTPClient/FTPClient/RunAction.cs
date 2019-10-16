using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTPClient
{
    class RunAction
    {
        public void Run()
        {
            //FTP操作           
            LogAction log = new LogAction(@"d:\", "ftplog");
            log.CreateLogFile("=================================FTP操作=================================");
            FolderAction folderAction = new FolderAction();
            var fpath = folderAction.CreateFolder(@"E:\FTPRever");
            Console.WriteLine("文件夹的路径是{0}", fpath);
            
            string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            string ftpFolder = string.Format($"{getDate}T030000");
            //string[] dbList = {"aspnetdb","JJ_Communication","JJ_Sale","JJ_System","JJlinshi"};
            string[] dbList = { "JJ_Communication", "JJ_Sale", "JJ_System" };
            string uname = "autoDL";
            string pwd = "123";
            foreach (var item in dbList)
            {
                
                string ftppath = string.Format(@"ftp://10.12.2.9/{0}/{1}", ftpFolder, item);
                string logcontext = string.Format($"FTP下载路径{ftppath}");
                log.CreateLogFile(logcontext);
                //Console.WriteLine("生成的路径是{0});
                FtpAction ftp = new FtpAction(ftppath, uname, pwd);
                string localpath = string.Format(@"{0}\{1}", fpath, item);
                logcontext = string.Format($"下载文件所在位置{localpath}");
                log.CreateLogFile(logcontext);
                var starttime = DateTime.Now;
                log.CreateLogFile(string.Format($"{ftppath}开始下载的时间{starttime}"));
                ftp.FtpDownLocal(localpath);
                var endtime = DateTime.Now;
                log.CreateLogFile(string.Format($"{ftppath}结束下载的时间{endtime}"));
                var usetime = starttime - endtime;
                log.CreateLogFile(string.Format($"{ftppath}下载话费的时间{usetime}"));
            }
            //数据库操作
            IConnectionDb connectionDb = new SQLBackupAction();
            IRevertDbAction revertDb = new SQLBackupAction();
            var connet = connectionDb.InitConnection();
            var dbinfos = revertDb.GetDbrevert(connet, fpath, dbList);
            log.CreateLogFile("=================================数据库操作=================================");
            foreach (var itemdbinfo in dbinfos)
            {
                log.CreateLogFile(string.Format($"正在还原数据库{itemdbinfo.DbName}"));
                var starttime = DateTime.Now;
                log.CreateLogFile(string.Format($"还原数据库{itemdbinfo.DbName}开始时间{starttime}"));
                revertDb.AutoRevert(itemdbinfo, connet);
                var endtime = DateTime.Now;
                log.CreateLogFile(string.Format($"还原数据库{itemdbinfo.DbName}结束时间{endtime}"));
                var usetime = starttime - endtime;
                log.CreateLogFile(string.Format($"还原数据库{itemdbinfo.DbName}使用时间{usetime}"));
            }
            connectionDb.Closeconnection(connet);

            Console.WriteLine("开始删除数据库文件");
            log.CreateLogFile($"{DateTime.Now}删除接收到的数据库文件");
            folderAction.DeleteFolder(fpath);
        }
    }
}

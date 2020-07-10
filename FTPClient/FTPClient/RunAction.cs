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
            //这个版本是用在10.12.2.19这台服务器上的
            LogAction log = new LogAction(@"d:\", "ftplog.txt");
            log.CreateLogFile("\n=================================FTP操作=================================\n");
            FolderAction folderAction = new FolderAction();
            var fpath = folderAction.CreateFolder(@"E:\FTPRever");
            //var fpath = folderAction.CreateFolder(@"D:\FTPRever");
            Console.WriteLine("文件夹的路径是{0}", fpath);
            
            string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            string ftpFolder = string.Format($"{getDate}T010000");
            //string[] dbList = {"aspnetdb","JJ_Communication","JJ_Sale","JJ_System","JJlinshi"};
            //string[] dbList = { "JJ_Communication", "JJ_Sale", "JJ_System" };
            string[] dbList = { "JJ_Sale" };
            string uname = "autoDL";
            string pwd = "123";
            foreach (var item in dbList)
            {
                
                string ftppath = string.Format(@"ftp://10.12.2.9/{0}/{1}", ftpFolder, item);
                string logcontext = string.Format($"FTP下载路径{ftppath}\n");
                log.CreateLogFile(logcontext);
                //Console.WriteLine("生成的路径是{0});
                FtpAction ftp = new FtpAction(ftppath, uname, pwd);
                string localpath = string.Format(@"{0}\{1}", fpath, item);
                logcontext = string.Format($"下载文件所在位置{localpath}\n");
                log.CreateLogFile(logcontext);
                var starttime = DateTime.Now;
                log.CreateLogFile(string.Format($"{ftppath}开始下载的时间{starttime}\n"));
                ftp.FtpDownLocal(localpath);
                var endtime = DateTime.Now;
                log.CreateLogFile(string.Format($"{ftppath}结束下载的时间{endtime}\n"));
                var usetime = starttime - endtime;
                log.CreateLogFile(string.Format($"{ftppath}下载话费的时间{usetime}\n"));
            }
            //数据库操作
            IConnectionDb connectionDb = new SQLBackupAction();
            IRevertDbAction revertDb = new SQLBackupAction();
            var connet = connectionDb.InitConnection();
            var dbinfos = revertDb.GetDbrevert(connet, fpath, dbList);
            log.CreateLogFile("\n=================================数据库操作=================================\n");
            foreach (var itemdbinfo in dbinfos)
            {
                log.CreateLogFile(string.Format($"\n正在还原数据库{itemdbinfo.DbName}\n"));
                var starttime = DateTime.Now;
                log.CreateLogFile(string.Format($"\n还原数据库{itemdbinfo.DbName}开始时间{starttime}\n"));
                revertDb.AutoRevert(itemdbinfo, connet);
                var endtime = DateTime.Now;
                log.CreateLogFile(string.Format($"\n还原数据库{itemdbinfo.DbName}结束时间{endtime}\n"));
                var usetime = starttime - endtime;
                log.CreateLogFile(string.Format($"\n还原数据库{itemdbinfo.DbName}使用时间{usetime}\n"));
            }
            connectionDb.Closeconnection(connet);

            Console.WriteLine("开始删除数据库文件");
            log.CreateLogFile($"{DateTime.Now}删除接收到的数据库文件\n");
            folderAction.DeleteFolder(fpath);
        }
    }
}

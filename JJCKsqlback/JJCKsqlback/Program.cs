﻿using System;
using System.IO;
using System.Threading;



namespace JJCKsqlback
{
    class Program
    {
        /// <summary>
        /// 通过调用委托，将自动备份方法传入定时方法中
        /// </summary>
        /// <param name="args"></param>
        /// 
        static void Main(string[] args)
        {
            #region 这段代码是数据库定时备份程序以及每隔4天删除文件的调用程序
            //SqlAction sqlAction = new SqlAction();
            //FileAction fileAction = new FileAction();
            //SQLshell autobackshell = sqlAction.DataBaseContronal;
            //SQLshell autodeleteFile = fileAction.FileShell;
            //ClockTime clockTime = new ClockTime();
            //var setRunTime = clockTime.SetRunDay(4);
            //while (true)
            //{
            //    clockTime.TimeCompare(autobackshell);//这个是自动备份的委托调用 
            //    clockTime.DayCompare(autodeleteFile, setRunTime); //这个是删除4天的文件夹
            //    //clockTime.DayCompare(autodeleteFile);//这个是每隔四天就要删除一次文件。
            //}
            #endregion
            #region 这段代码是定时删除图片缓存文件
            //FileAction fileAction = new FileAction();
            //SQLshell autodeleteshell = fileAction.DeleteFileShell;
            //ClockTime clockTime = new ClockTime();
            //var setRunMonth = clockTime.SetRunMonth(3);
            //while (true)
            //{
            //    clockTime.MonthCompare(autodeleteshell, setRunMonth);

            //}
            #endregion
            #region 这段代码是自动还原数据库的
            //SqlAction action = new SqlAction();
            //action.DataBaseRevert();
            #endregion
            #region 这段代码是SOCKET客户端获取文件，发送文件的代码
            IFolder folderAction = new FolderAction();
            ISockEumFiles eumFiles = new FolderAction();
            IReadFile read = new FolderAction();
            var floderPath=folderAction.CreateFolderPath(@"f:\test");
            Console.WriteLine("生产的文件路径{0}",floderPath);
            var socketfilePath=eumFiles.EumFile(floderPath);
            foreach (var item in socketfilePath)
            {
                Console.WriteLine("获取到的文件名称{0}",item.FullName);
            }
            var socketfilePack = read.ReadFile(socketfilePath);
            SClient client = new SClient();
            client.Client(socketfilePack, new HostInfo { Port = 2000, ServerHost = "127.0.0.1" });
            #endregion
            Console.ReadKey();



        }
    }
}

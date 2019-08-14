using System;
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
           
            SqlAction sqlAction = new SqlAction();
            FileAction fileAction = new FileAction();
            SQLshell autobackshell = sqlAction.DataBaseContronal;
            SQLshell autodeleteFile = fileAction.FileShell;
            //SQLshell autodeleteshell = fileAction.TestShell;
            //SQLshell autobackshell = sqlAction.Test;
            ClockTime clockTime = new ClockTime();
            var setRunTime = clockTime.SetRunDay(4);
            while (true)
            {               
                clockTime.TimeCompare(autobackshell);//这个是自动备份的委托调用 
                clockTime.DayCompare(autodeleteFile, setRunTime); //这个是删除4天的文件夹
                //clockTime.DayCompare(autodeleteFile);//这个是每隔四天就要删除一次文件。
            }
            //Console.ReadKey();

            

        }
    }
}

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
        static void Main(string[] args)
        {
            SqlAction sqlAction = new SqlAction();
            SQLshell autobackshell = sqlAction.DataBaseContronal;           
            while (true)
            {
                ClockTime clockTime = new ClockTime();
                clockTime.TimeCompare(autobackshell);
                Thread.Sleep(100);
                Console.Clear();
            }
            //Console.ReadKey();

            

        }
    }
}

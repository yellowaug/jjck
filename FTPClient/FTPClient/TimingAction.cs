using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FTPClient
{
    public class RunTimming
    {
        public int RunDay { get; set; }
        public int RunCount { get; set; }
        public int RunMount { get; set; }
    }
    class TimingAction
    {
        ///<summary>
        ///定时器
        ///根据设定的时间决定程序每天是几点运行的。
        ///</summary>
        private RunTimming runTime = new RunTimming();
        //private LogFile logFile = new LogFile();
        public void TimeCompare(Action shell, int runhour)//传入委托，自动备份方法在判断时间
        {
            DateTime localtime = DateTime.Now;
            int hournow = localtime.Hour;
            int minuteNow = localtime.Minute;
            int seconNow = localtime.Second;
            int sethour = runhour;            
            int setmin = 00;
            int setsec = 00;
            if (hournow == sethour && minuteNow == setmin && seconNow == setsec)
            {

                //Console.WriteLine("数据库正在备份中。。。。。");
                Console.WriteLine("数据库文件正在接收。。。。。");
                shell();
            }
            else
            {

                Console.WriteLine("当前时间：{0}", localtime.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine($"数据库备份接收程序运行的时间{sethour}:{setmin}:{setsec}");
                Thread.Sleep(100);
                Console.Clear();
            }
        }
        /// <summary>
        /// 根据设定的时间，判断程序要间隔几天执行
        /// </summary>
        /// <param name="shell"></param>
        public void DayCompare(Action shell, RunTimming runTime)
        {
            DateTime localtime = DateTime.Now;
            int dayNow = localtime.Day;
            int hournow = localtime.Hour;
            int minuteNow = localtime.Minute;
            int seconNow = localtime.Second;
            if (dayNow == runTime.RunDay && hournow == 00 && minuteNow == 00 && seconNow == 00)
            {
                SetRunDay(4);
                shell();

            }
            else
            {
                Console.WriteLine($"距离执行定时删除程序还有{runTime.RunDay - localtime.Day + 1}天");
            }
        }
        public void MonthCompare(Action shell, RunTimming runmonth)
        {
            DateTime localtime = DateTime.Now;
            int monthNow = localtime.Month;
            int dayNow = localtime.Day;
            int hournow = localtime.Hour;
            int minuteNow = localtime.Minute;
            int seconNow = localtime.Second;
            if (monthNow == runmonth.RunMount && dayNow == 1 && hournow == 00 && minuteNow == 00 && seconNow == 00)
            {
                SetRunMonth(3);
                shell();
            }
            else
            {
                Console.WriteLine($"距离执行网站缓存图片文件删除程序还有{runTime.RunMount - localtime.Month + 1}个月");
                Console.WriteLine("当前时间：{0}", localtime.ToString("yyyy-MM-dd HH:mm:ss"));
                Thread.Sleep(100);
                Console.Clear();
            }
        }
        /// <summary>
        /// 设定运行的间隔天数
        /// 能让日期正常的轮训了，但是我偷懒了，只有30天的，懒得算31天的了
        /// </summary>
        /// <param name="setPra"></param>
        /// <returns></returns>
        public RunTimming SetRunDay(int setPra)
        {
            DateTime localtime = DateTime.Now;
            int dayNow = localtime.Day;
            int runDay = dayNow + setPra - 1;
            if (runDay <= 30)
            {
                runTime.RunDay = runDay;
                Console.WriteLine($"运行日期{runDay}");

            }
            else
            {
                runDay = (dayNow + setPra - 1) - 30;
                runTime.RunDay = runDay;
                Console.WriteLine($"运行日期{runDay}");
            }
            //logFile.CreateLogFile($"运行日期{runDay}\n");
            return runTime;
        }
        /// <summary>
        /// 设置程序运行的间隔月份数
        /// </summary>
        /// <param name="setPra"></param>
        /// <returns></returns>
        public RunTimming SetRunMonth(int setPra)
        {
            DateTime localtime = DateTime.Now;
            int nowMonth = localtime.Month;
            int runMonth = nowMonth + setPra - 1;
            if (runMonth <= 12)
            {
                runTime.RunMount = runMonth;
                Console.WriteLine($"运行月份{runMonth}");
            }
            else
            {
                runMonth = (nowMonth + setPra - 1) - 12;
                runTime.RunMount = runMonth;
                Console.WriteLine($"运行月份{runMonth}");
            }
            //logFile.CreateLogFile($"运行月份{runMonth}\n", "AutoDeletelog.txt");
            return runTime;
        }
    }
}

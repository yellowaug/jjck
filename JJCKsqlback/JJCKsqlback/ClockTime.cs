using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace JJCKsqlback
{
    delegate void SQLshell();
    class ClockTime
    {
        ///<summary>
        ///定时器
        ///根据设定的时间决定程序每天是几点运行的。
        ///</summary>
        public void TimeCompare(SQLshell shell)//传入委托，自动备份方法在判断时间
        {
            DateTime localtime = DateTime.Now;
            int hournow = localtime.Hour;
            int minuteNow = localtime.Minute;
            int seconNow = localtime.Second;
            if (hournow==16&&minuteNow==54&&seconNow==00)
            {
                               
                Console.WriteLine("数据库正在备份中。。。。。");
                shell();
            }
            else
            {

                Console.WriteLine("当前时间：{0}",localtime.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine("程序运行的时间03:00:00");
                Thread.Sleep(100);
                Console.Clear();
            }
        }
        /// <summary>
        /// 根据设定的时间，判断程序要间隔几天执行
        /// </summary>
        /// <param name="shell"></param>
        public void DayCompare(SQLshell shell,RunTimeData runTime)
        {
            DateTime localtime = DateTime.Now;
            int dayNow = localtime.Day;
            if (dayNow == runTime.RunDay)
            {
                
                var runday=SetRunDay();
                runday.RunCount = 1;
                if (runday.RunCount==1)
                {
                    shell();
                    runday.RunCount = 0;
                }
            }
            else
            {
                Console.WriteLine($"距离执行该程序还有{runTime.RunDay-localtime.Day}天");

            }


        }
        public RunTimeData SetRunDay()
        {
            RunTimeData runTime = new RunTimeData();
            DateTime localtime = DateTime.Now;
            int dayNow = localtime.Day;
            int runDay = dayNow + 4 - 1;
            runTime.RunDay = runDay;
            return runTime;
        }
        
    }
}

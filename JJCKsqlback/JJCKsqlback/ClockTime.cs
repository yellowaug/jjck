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
        private RunTimming runTime = new RunTimming();
        public void TimeCompare(SQLshell shell)//传入委托，自动备份方法在判断时间
        {
            DateTime localtime = DateTime.Now;
            int hournow = localtime.Hour;
            int minuteNow = localtime.Minute;
            int seconNow = localtime.Second;
            int sethour = 03;
            int setmin = 00;
            int setsec = 00;
            if (hournow==sethour&&minuteNow==setmin&&seconNow==setsec)
            {
                               
                Console.WriteLine("数据库正在备份中。。。。。");
                shell();
            }
            else
            {

                Console.WriteLine("当前时间：{0}",localtime.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine($"数据库备份程序运行的时间{sethour}:{setmin}:{setsec}");
                Thread.Sleep(100);
                Console.Clear();
            }
        }
        /// <summary>
        /// 根据设定的时间，判断程序要间隔几天执行
        /// </summary>
        /// <param name="shell"></param>
        public void DayCompare(SQLshell shell, RunTimming runTime)
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
                Console.WriteLine($"距离执行定时删除程序还有{runTime.RunDay-localtime.Day+1}天");
            }


        }
        /// <summary>
        /// 设定运行的间隔天数
        /// </summary>
        /// <param name="setPra"></param>
        /// <returns></returns>
        public RunTimming SetRunDay(int setPra)
        {
            DateTime localtime = DateTime.Now;            
            int dayNow = localtime.Day;
            int runDay = dayNow + setPra - 1;
            runTime.RunDay = runDay;
            return runTime;
        }
        
    }
}

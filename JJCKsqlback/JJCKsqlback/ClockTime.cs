using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (hournow==11&&minuteNow==57&&seconNow==00)
            {
                               
                Console.WriteLine("数据库正在备份中。。。。。");
                shell();
            }
            else
            {
                Console.WriteLine(localtime.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }
        /// <summary>
        /// 根据设定的时间，判断程序要间隔几天执行
        /// </summary>
        /// <param name="shell"></param>
        public void DayCompare(SQLshell shell)
        {
            DateTime localtime = DateTime.Now;
            int dayNow = localtime.Day;
            var comPare = dayNow % 4;
            Console.WriteLine(comPare);
            if (comPare == 0)
            {
                shell();
            }
            else
            {
                Console.WriteLine(localtime.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }
        
    }
}

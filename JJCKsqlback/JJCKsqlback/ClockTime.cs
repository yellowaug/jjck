using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JJCKsqlback
{
    class ClockTime
    {
        ///<summary>
        ///定时器
        ///一个简单的定时运行的程序算是实现了
        ///</summary>
        public void TimeCompare()
        {
            DateTime localtime = DateTime.Now;
            int hournow = localtime.Hour;
            int minuteNow = localtime.Minute;
            int seconNow = localtime.Second;
            if (hournow==23&&minuteNow==30&&seconNow==00)
            {
                Console.WriteLine("就是这个时候了");
                Console.WriteLine("数据库正在备份中。。。。。");
            }
            else
            {
                Console.WriteLine(localtime.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }
        
    }
}

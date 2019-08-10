using System;
using System.IO;
using System.Threading;



namespace JJCKsqlback
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ClockTime clockTime = new ClockTime();
                clockTime.TimeCompare();
                Thread.Sleep(100);
                Console.Clear();
            }
            //Console.ReadKey();

            

        }
    }
}

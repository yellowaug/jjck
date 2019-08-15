using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JJCKsqlback
{

    class LogFile
    {
        /// <summary>
        /// 日志文件生成，以及日志内容写入,日志默认在E盘
        /// </summary>
        /// <param name="logContext"></param>
        public void CreateLogFile(string logContext)
        {
            string logpath = @"e:\";
            string fileFullPath = Path.Combine(logpath, "AutoBackupBDLog.txt");
            try
            {
                File.AppendAllText(fileFullPath, logContext);
                Console.WriteLine("日志写入成功");
            }
            catch (ArgumentException argexc)
            {

                Console.WriteLine(argexc);
            }
            catch (DirectoryNotFoundException direxcp)
            {
                Console.WriteLine(direxcp);
            }
            catch (IOException ioexcp)
            {
                Console.WriteLine(ioexcp);
            }
            catch (UnauthorizedAccessException unauexcp)
            {
                Console.WriteLine(unauexcp);
            }
            catch(NotSupportedException notsupexc)
            {
                Console.WriteLine(notsupexc);
            }
        }
        /// <summary>
        /// 这个方法可以自定义传入内容以及日志的文件名称，日志的默认路径在D盘
        /// </summary>
        /// <param name="logContext"></param>
        /// <param name="logFileName"></param>
        public void CreateLogFile(string logContext,string logFileName)
        {
            string logpath = @"d:\";
            string fileFullPath = Path.Combine(logpath, logFileName);
            try
            {
                File.AppendAllText(fileFullPath, logContext);
                Console.WriteLine("日志写入成功");
            }
            catch (ArgumentException argexc)
            {

                Console.WriteLine(argexc);
            }
            catch (DirectoryNotFoundException direxcp)
            {
                Console.WriteLine(direxcp);
            }
            catch (IOException ioexcp)
            {
                Console.WriteLine(ioexcp);
            }
            catch (UnauthorizedAccessException unauexcp)
            {
                Console.WriteLine(unauexcp);
            }
            catch (NotSupportedException notsupexc)
            {
                Console.WriteLine(notsupexc);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FTPClient
{
    public class LogAction
    {
        private string DiskPath { get; set; }
        private string LogFileName { get; set; }
        public LogAction(string diskpath,string logfilename)
        {
            this.DiskPath = diskpath;
            this.LogFileName = logfilename;
        }
        /// <summary>
        /// 这个方法可以自定义传入内容以及日志的文件名称，日志的默认路径在D盘
        /// </summary>
        /// <param name="logContext"></param>
        /// <param name="logFileName"></param>
        public void CreateLogFile(string logContext)
        {
            string fileFullPath = Path.Combine(this.DiskPath, this.LogFileName);
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketServerReCode
{
    public class SocketFolderAction
    {
        public string RootPath { get; set; }
        public int FileSize { get; set; }
        public string FileName { get; set; }
        public SocketFolderAction() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootpath">数据库所在的根目录路径</param>
        public SocketFolderAction(string rootpath)
        {
            this.RootPath = rootpath;
        }
        /// <summary>
        /// 生成文件夹路径并判断是否存在
        /// </summary>
        /// <returns></returns>
        public string GenerateFolderPath()
        {
            
            string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            Console.WriteLine($"获取到的日期是{getDate}");
            string folderName = String.Format($"{getDate}T030000");
            Console.WriteLine($"生成的文件名为{folderName}");
            string path = Path.Combine(this.RootPath, folderName);
            var existsFolder = Directory.Exists(path);
            if (existsFolder == true)
            {
                return path;
            }
            else
            {
                return "文件夹不存在";
            }
        }
        /// <summary>
        /// 获取文件夹中的文件个数
        /// </summary>
        /// <param name="folderPath">生成的文件夹路径</param>
        /// <returns>枚举对象</returns>
        public IEnumerable<FileInfo> EnumFile(string folderPath)
        {
            DirectoryInfo directory = new DirectoryInfo(folderPath);
            var fileinfoEnu = directory.EnumerateFiles();
            if (fileinfoEnu != null)
            {
                foreach (var file in fileinfoEnu)
                {
                    Console.WriteLine("文件路径：{0}",file.FullName);
                    Console.WriteLine("文件大小：{0}",file.Length);
                }
            }
            else
            {
                Console.WriteLine("文件夹为空");
            }
            return fileinfoEnu;
        }
        /// <summary>
        /// 发送文件名，文件夹中的文件个数，文件大小
        /// </summary>
        /// <param name="filesObj">枚举对象</param>
        /// <param name="action">发送数据的委托</param>
        /// <param name="socket">socket配置信息</param>
        public void GenerateFileInof(IEnumerable<FileInfo> filesObj, Action<Socket, byte[]> action, Socket socket)
        {
            byte[] packFileCount = new ASCIIEncoding().GetBytes(filesObj.Count().ToString());
            action.Invoke(socket, packFileCount);
            foreach (var fileinfo in filesObj)
            {                
                byte[] packFileinfo = new ASCIIEncoding().GetBytes(fileinfo.Name);
                action.Invoke(socket, packFileinfo);
                byte[] packFileSize = new ASCIIEncoding().GetBytes(fileinfo.Length.ToString());
                action.Invoke(socket, packFileSize);
            }
        }
        /// <summary>
        /// 发送数据包，分三种情况：文件大小%65536=0的时候，文件大小%6553<1的时候,文件大小%65536>1&!=0的时候
        /// </summary>
        /// <param name="fileObj">枚举对象</param>
        /// <param name="action">发送数据的委托</param>
        /// <param name="socket">socket配置信息</param>
        public void GenerateDataPack(IEnumerable<FileInfo> fileObj,Action<Socket, byte[]> action,Socket socket)
        {
            
            foreach (var itemfile in fileObj)
            {
                var fileStearm = File.OpenRead(itemfile.FullName);
                Console.WriteLine("现在读取的是{0}\t文件大小{1}",itemfile.FullName,itemfile.Length);

                int packLength = (int)itemfile.Length % 65536;
                Console.WriteLine($"求得的余数是{packLength}");
                if (packLength>0&&packLength<1)
                {
                    byte[] dataPack = new byte[(int)itemfile.Length];
                    fileStearm.Read(dataPack, 0, dataPack.Length);
                    action.Invoke(socket,dataPack);//调用socket发送数据的委托
                }
                else if (packLength==0)
                {
                    int dataPackCount = (int)itemfile.Length / 65536;
                    for (int i = 0; i < dataPackCount; i++)
                    {
                        byte[] dataPack = new byte[65536];
                        fileStearm.Read(dataPack, 0, dataPack.Length);
                        action.Invoke(socket, dataPack);
                    }
                }
                else if (packLength!=0&&packLength>1)
                {
                    int dataPackCount = (int)itemfile.Length / 65536;
                    Console.WriteLine("发送文件循环的次数为{0}",dataPackCount);
                    for (int i = 0; i < dataPackCount; i++)
                    {
                        byte[] dataPack = new byte[65536];
                        fileStearm.Read(dataPack, 0, dataPack.Length);
                        action.Invoke(socket, dataPack);
                    }
                    Console.WriteLine("发送最后一个数据包");
                    byte[] lastDataPack = new byte[packLength];
                    fileStearm.Read(lastDataPack, 0, lastDataPack.Length);
                    action.Invoke(socket, lastDataPack);
                }
                fileStearm.Close();
            }
        }
    }
}

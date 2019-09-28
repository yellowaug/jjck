using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JJCKsqlback
{
    /// <summary>
    /// 存储文件名列表，文件大小列表，数据包列表
    /// </summary>
    public class FilePack
    {
        public List<string> FileName { get; set; }
        public List<int> FileSize { get; set; }
        public List<TempPack> FilePackList { get; set; }
    }
    /// <summary>
    /// 存储未封装的数据包
    /// </summary>
    public class TempPack
    {
        public List<byte[]> TempFilePackList { get; set; }
    }
    /// <summary>
    /// 读取文件夹中有多少文件的接口
    /// </summary>
    public interface ISockEumFiles
    {
        IEnumerable<FileInfo> EumFile(string folderPath);
    }
    public interface IFolder
    {
        string CreateFolderPath(string diskPath);
    }
    /// <summary>
    /// 读取文件内容封装成数据包的接口
    /// </summary>
    public interface IReadFile
    {
        FilePack ReadFile(IEnumerable<FileInfo> filePathlist);
    }
    public class FolderAction : ISockEumFiles, IReadFile,IFolder
    {
        /// <summary>
        /// 根据当天的日期生成要获取的文件夹名称
        /// </summary>
        /// <param name="diskPath">要扫描的路径</param>
        /// <returns>返回路径名称</returns>
        string IFolder.CreateFolderPath(string diskPath)
        {
            string getDate = DateTime.Now.Date.ToString("yyyyMMdd");
            Console.WriteLine($"获取到的日期是{getDate}");
            string folderName = String.Format($"{getDate}T030000");
            Console.WriteLine($"生成的文件名为{folderName}");
            string path = Path.Combine(diskPath, folderName);
            var existsFolder = Directory.Exists(path);
            if (existsFolder==true)
            {
                return path;
            }
            else
            {
                return "文件夹不存在";
            }
        }

        IEnumerable<FileInfo> ISockEumFiles.EumFile(string folderPath)
        {
            DirectoryInfo directory = new DirectoryInfo(folderPath);
            var fileinfoEnu = directory.EnumerateFiles();
            if (fileinfoEnu != null)
            {
                foreach (var file in fileinfoEnu)
                {
                    Console.WriteLine(file.FullName);
                    Console.WriteLine(file.Length);
                }
            }
            else
            {
                Console.WriteLine("文件夹为空");
            }
            return fileinfoEnu;
        }



        //将大文件切割成1024字节大小的小数据包，因为TCP/IP传输的数据包大小是有限制的
        FilePack IReadFile.ReadFile(IEnumerable<FileInfo> filePathlist)
        {
            FilePack filePack = new FilePack();


            List<string> fullName = new List<string>();
            List<int> fileSize = new List<int>();
            List<TempPack> tempPacks = new List<TempPack>();
            foreach (var item in filePathlist)
            {
                TempPack tempPackList = new TempPack();
                fullName.Add(item.Name);
                fileSize.Add((int)item.Length);
                tempPacks.Add(tempPackList);
                List<byte[]> fileInfoPackList = new List<byte[]>();
                int packLength = (int)item.Length % 1024;
                if (packLength == 0)
                {
                    var fs = File.OpenRead(item.FullName);
                    for (int i = 0; i < (int)item.Length / 1024; i++)
                    {
                        byte[] fscontext = new byte[1024];
                        fs.Read(fscontext, 0, fscontext.Length);
                        fileInfoPackList.Add(fscontext);
                    }
                    fs.Close();
                    tempPackList.TempFilePackList = fileInfoPackList;

                    //byte[] fscontext = new byte[item.Length];
                    Console.WriteLine(item.Length);
                    //var fs = File.OpenRead(item.FullName);
                }
                else if (packLength != 0)
                {
                    var fs = File.OpenRead(item.FullName);
                    int ts = (int)item.Length / 1024;
                    for (int i = 0; i < ts; i++)
                    {
                        byte[] fscontext = new byte[1024];
                        fs.Read(fscontext, 0, fscontext.Length);
                        fileInfoPackList.Add(fscontext);

                    }

                    byte[] lastPack = new byte[(int)item.Length % 1024];
                    fs.Read(lastPack, 0, lastPack.Length);
                    fileInfoPackList.Add(lastPack);
                    fs.Close();
                    tempPackList.TempFilePackList = fileInfoPackList;

                }

            }
            filePack.FileName = fullName;
            filePack.FileSize = fileSize;
            filePack.FilePackList = tempPacks;
            return filePack;




        }
    }
}

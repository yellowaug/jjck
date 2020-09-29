using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMoveFile
{
    public class FolderInfo
    {
        public List<string> fileList { get; set; }
        public List<string> folderList { get; set; }
        public FolderInfo() { }
        public FolderInfo(List<string> fileList,List<string> folderList)
        {
            this.fileList = fileList;
            this.folderList = folderList;
        }

    }
    public class FolderAction
    {
        
        public void CheckFile(List<string> folderPathList)
        {
            foreach (var folderpath in folderPathList)
            {
                var existsFolder = Directory.Exists(folderpath);
                if (existsFolder)
                {
                    var fileList = Directory.GetFiles(folderpath);
                    List<string> dirs = new List<string>(Directory.EnumerateDirectories(folderpath));
                    foreach (var folder in dirs)
                    {
                        Console.WriteLine(folder);
                        Console.WriteLine(folder.Substring(11));
                        var folderName = folder.Substring(11);
                        string desPath = string.Format(@"G:\testEnvCopy\{0}", folderName); //目标路径
                        Directory.Move(folder, desPath);

                    }
                }
            }
            
        }
        //public void MoveFileAct(string filePath)
        //{
           
        //}
    }
}

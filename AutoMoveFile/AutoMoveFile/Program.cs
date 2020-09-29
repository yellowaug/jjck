using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMoveFile
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadXml readXml = new ReadXml();
            FolderAction folder = new FolderAction();
            List<string> folderpath = readXml.SetConfig();
            folder.CheckFile(folderpath);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlRead xml = new XmlRead();
            string folderPath= xml.ReadXmlPath();
            CreateFolder createFolder = new CreateFolder();
            createFolder.CreateFolderAction(folderPath);
            Console.ReadKey();
        }
    }
}

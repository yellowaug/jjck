using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJCKsqlback
{
    class FileAction
    {
        public void FileShell()
        {
            ///<summary>
            ///获取子文件夹列表,并删除文件夹以及文件
            ///</summary>
            IGetFolderInfo folderInfo = new Folder();
            IEumFiles eumFiles = new Folder();
            IDeletFolder deletFolder = new Folder();
            string filepath = @"E:\DataBase_AutoBak"; //这个路径记当换机子的时候记得修改
            var floderobjinfo = folderInfo.GetFolderInfo(filepath);
            for (int i = 0; i < floderobjinfo.Count - 1; i++)
            {
                deletFolder.Delete(floderobjinfo[i].FullPath);

            }
            foreach (var folder in floderobjinfo)
            {
                //Console.WriteLine(folder.FolderName);
                //Console.WriteLine(folder.RootPath);
                //Console.WriteLine(folder.FullPath);
                Console.WriteLine("==========================");
                eumFiles.EumFile(folder);
                deletFolder.Delete(folder.FullPath);

            }
        }
        public void TestShell()
        {
            Console.WriteLine("测试方法运行成功");
        }

    }
}

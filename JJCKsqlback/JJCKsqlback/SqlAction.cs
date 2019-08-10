using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJCKsqlback
{
    /// <summary>
    /// 数据库的相关操作全部在这个类里，备份数据库，删除过往数据库文件
    /// </summary>
    class SqlAction
    {
        public void DataBaseContronal()
        {
            ///<summary>
            ///创建文件，返回文件路径
            ///string filepath 文件存放的路径
            ///</summary>
            string filepath = @"D:\sqlbak";
            ICreateFolder createFolder = new Folder();
            string path = createFolder.Create(filepath);
            ///<summary>
            ///连接数据库
            ///</summary>
            IConnectionDb connectionDb = new DataBase();
            var connection = connectionDb.InitConnection();


            ///<summary>
            ///生成备份语句
            ///string[] backdblist 这个是要备份的数据库名称的数组，一般改这就好了
            ///</summary>
            string[] backdblist = { "aspnetdb", "JJ_Annex", "JJ_Communication", "JJ_Sale", "JJ_System" };
            //string[] backdblist = { "Account", "Book", "EFtest" };
            IGetDbList getDbList = new DataBase();
            var dbList = getDbList.GetDb(connection, path, backdblist);

            ///<summary>
            ///创建自动备份程序
            ///</summary>
            IAutoBackUp autoBack = new DataBase();
            foreach (var itemDblist in dbList)
            {
                DateTime begenTime = DateTime.Now;
                autoBack.AutoBack(itemDblist, connection);
                DateTime endTime = DateTime.Now;
                var usetime = begenTime - endTime;
                Console.WriteLine($"数据库备份所用时间{usetime}");//计算所用时间
            }

            //currentPath.SetPath(path);
            ///<summary>
            ///关闭数据库连接，回收资源
            ///</summary>
            ICloseConnection close = new DataBase();
            close.Closeconnection(connection);


            ///<summary>
            ///获取子文件夹列表,并删除文件夹以及文件
            ///</summary>
            //IGetFolderInfo folderInfo = new Folder();
            //IEumFiles eumFiles = new Folder();
            //IDeletFolder deletFolder = new Folder();
            //var floderobjinfo = folderInfo.GetFolderInfo(filepath);
            //for (int i = 0; i < floderobjinfo.Count - 1; i++)
            //{
            //    deletFolder.Delete(floderobjinfo[i].FullPath);

            //}
            //foreach (var folder in floderobjinfo)
            //{
            //    //Console.WriteLine(folder.FolderName);
            //    //Console.WriteLine(folder.RootPath);
            //    //Console.WriteLine(folder.FullPath);
            //    Console.WriteLine("==========================");
            //    eumFiles.EumFile(folder);
            //    deletFolder.Delete(folder.FullPath);

            //}

            ///<summary>
            ///文件删除
            ///</summary>
        }
    }
}

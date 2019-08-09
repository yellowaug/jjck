using System;
using System.IO;



namespace JJCKsqlback
{
    class Program
    {
        static void Main(string[] args)
        {
            ICreateFolder createFolder = new Folder();
            //IGetFolderInfo getinfo = new Folder();
            //ICurrentPath currentPath = new Folder();
            IAutoBackUp autoBack = new DataBase();
            string path = createFolder.Create();
            //var folderlist=getinfo.GetFolderInfo();
            //string rootpath = @"c:\testFolder";
            
            //foreach (var folderitem in folderlist)
            //{
            //    Console.WriteLine($"外部的:{folderitem.Name}");
            //    string parpath = Path.Combine(rootpath, folderitem.Name);
            //    currentPath.SetPath(parpath);
            //}
            autoBack.AutoBack(path);
            //currentPath.SetPath(path);
            Console.ReadKey();
        }
    }
}

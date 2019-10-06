using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SocketServerReCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var socketServer = SocketServer.ConfigSocket(2500, "0.0.0.0", 50);


            SocketFolderAction folderAction = new SocketFolderAction(@"F:\testsendFile");
            string folderPath=folderAction.GenerateFolderPath();
            var fileList=folderAction.EnumFile(folderPath);

            var s = new SocketServer();
            Action<Socket, byte[]> action = s.SendPack;

            folderAction.GenerateFileInof(fileList, action, socketServer);
            folderAction.GenerateDataPack(fileList, action, socketServer);

            Console.Read();


        }
    }
}

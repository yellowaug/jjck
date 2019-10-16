using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketClientReCode
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketFolderAction folderAction = new SocketFolderAction();
            SocketFileAction fileAction = new SocketFileAction();
            Action<byte[], string> writeAction = fileAction.WriteToFile;
            string folderFullName = folderAction.CreateFolder(@"D:\SocketDbFile");
            SocketClient socketClient = new SocketClient(2500, "10.11.11.39");
            socketClient.ClientAction(writeAction, folderFullName);
            Console.Read();
        }
    }
}

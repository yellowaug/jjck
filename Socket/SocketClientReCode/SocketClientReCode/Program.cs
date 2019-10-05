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
            //string folderFullName=folderAction.CreateFolder(@"G:\");
            SocketClient socketClient = new SocketClient(2500, "127.0.0.1");
            socketClient.ClientAction(writeAction, @"G:\");
            Console.Read();
        }
    }
}

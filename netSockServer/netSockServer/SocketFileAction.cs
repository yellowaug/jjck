using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace socketServer
{

    public delegate void FileAction(byte[] recvFileCon, string sockfilePath);

    public class SocketFileAction
    {
        public void WriteToFile(byte[] recvFileCon, string sockfilePath)
        {
            var fs = File.Open(sockfilePath, FileMode.Append);
            //var fs = File.OpenWrite(@"e:\111sock.xps");
            fs.Write(recvFileCon, 0, recvFileCon.Length);
            fs.Close();
        }
    }

}


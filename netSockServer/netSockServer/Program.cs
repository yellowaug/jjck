using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace socketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ServerInfo info = new ServerInfo() { Host = "0.0.0.0", ServerPort = 2000, ListenBackLog = 50 };
                SocketServerAction socketServer = new SocketServerAction();
                
                socketServer.Socketserver(info);
            }
            

        }
    }


}

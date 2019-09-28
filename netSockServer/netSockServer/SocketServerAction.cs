using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace socketServer
{
    /// <summary>
    /// 服务器配置信息的属性类
    /// </summary>
    class ServerInfo
    {
        public int ServerPort { get; set; }
        public string Host { get; set; }
        public int ListenBackLog { get; set; }

    }
    class ServerRecvInfo
    {
        public List<int> FileSize { get; set; }
        public List<string> FileName { get; set; }
    }
    /// <summary>
    /// Socket服务器类
    /// </summary>
    class SocketServerAction
    {
        //private string fileName;
        //private int fileSize;
        /// <summary>
        /// 服务器端，接收来自客户端的发送文件，并保存在本地
        /// </summary>
        /// <param name="info"></param>
        public void Socketserver(ServerInfo info)
        {
            List<int> FileSize = new List<int>();
            List<string> FileName = new List<string>();
            //文件操作的委托
            FileAction WriteToFile = new SocketFileAction().WriteToFile;
            SFolderAction folderAction = new SocketFolderAction().CreateFolder;
            //配置服务器端的IP地址端口以及监听数量的信息
            int port = info.ServerPort;
            string host = info.Host;
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint iPEnd = new IPEndPoint(ip, port);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(iPEnd);
            s.Listen(info.ListenBackLog);
            Console.WriteLine("等待客户端连接");
            Socket temp = s.Accept();
            Console.WriteLine("连接建立");
            //接收客户端发来的文件数量
            string recvStr = "";
            byte[] recvPackLength = new byte[1024];
            int packsCount = temp.Receive(recvPackLength, recvPackLength.Length, 0);
            recvStr += new ASCIIEncoding().GetString(recvPackLength, 0, packsCount);
            Console.WriteLine("客户端要发送的文件数量：{0}个", recvStr);
            //temp.Shutdown(SocketShutdown.Both);
            //Thread.Sleep(1000);
            //接收客户端发来的文件名
            Socket filerecv = s.Accept();
            for (int i = 0; i < int.Parse(recvStr); i++)
            {
                string recvFileName = "";
                byte[] recvPackFileName = new byte[1024];
                packsCount = filerecv.Receive(recvPackFileName, recvPackFileName.Length, 0);
                recvFileName += new ASCIIEncoding().GetString(recvPackFileName, 0, packsCount);
                Console.WriteLine("从客户端发来的文件名：{0}", recvFileName);
                FileName.Add(recvFileName);
                //Thread.Sleep(1000);
                //for (int j = 0; j < this.fileSize; j++)
                //{
                //    byte[] recvByte = new byte[1024];
                //    temp.Receive(recvByte, recvByte.Length, 0);
                //    string buildFileName = Path.Combine(@"d:\", this.fileName);
                //    WriteToFile.Invoke(recvByte, buildFileName);
                //    float c = (float)j / (float)int.Parse(this.fileSize.ToString());
                //    Console.WriteLine($"文件接收{c * 100}%");
                //}

            }
            //filerecv.Close();
            Socket fileSizeRecv = s.Accept();
            //接收客户端发来的文件大小的数据包
            for (int i = 0; i < int.Parse(recvStr); i++)
            {
                string recvFileSize = "";
                byte[] recvPackFileSize = new byte[1024];
                packsCount = fileSizeRecv.Receive(recvPackFileSize, recvPackFileSize.Length, 0);
                recvFileSize += new ASCIIEncoding().GetString(recvPackFileSize, 0, packsCount);
                //this.fileSize = int.Parse(recvFileSize);
                FileSize.Add(int.Parse(recvFileSize));
                Console.WriteLine($"文件{FileName[i]}大小为：{recvFileSize}kb");
            }
            fileSizeRecv.Close();

            Console.WriteLine("接收文件大小数据完成");

            //文件写入的委托方法,开始接收文件数据包
            var recvFolderPath = folderAction.Invoke(@"D:\testsock");

            Socket filePackRecv = s.Accept();
            for (int i = 0; i < int.Parse(recvStr); i++)
            {
                Thread.Sleep(500);
                int filePack = FileSize[i] % 1024;
                if (filePack == 0)
                {
                    int filePackCount = FileSize[i] / 1024;
                    for (int j = 0; j < filePackCount; j++)
                    {
                        byte[] recvByte = new byte[1024];
                        filePackRecv.Receive(recvByte, recvByte.Length, 0);
                        //string buildFileName = Path.Combine(@"D:\testsock", FileName[i]);
                        string buildFileName = Path.Combine(recvFolderPath, FileName[i]);
                        //根据文件大小计算要切割的包大小
                        WriteToFile.Invoke(recvByte, buildFileName);
                        //调用委托
                        //recvByteList.Add(recvByte);
                        float c = (float)j / (float)filePackCount;
                        Console.WriteLine($"文件接收{c * 100}%");
                    }
                    Console.WriteLine("文件接收100%");
                }
                else if (filePack != 0)
                {
                    int filePackCount = FileSize[i] / 1024;
                    for (int j = 0; j < filePackCount; j++)
                    {
                        byte[] recvByte = new byte[1024];
                        filePackRecv.Receive(recvByte, recvByte.Length, 0);
                        //string buildFileName = Path.Combine(@"D:\testsock", FileName[i]);
                        string buildFileName = Path.Combine(recvFolderPath, FileName[i]);
                        //根据文件大小计算要切割的包大小
                        WriteToFile.Invoke(recvByte, buildFileName);
                        //调用委托
                        //recvByteList.Add(recvByte);
                        float c = (float)j / (float)filePackCount;
                        Console.WriteLine($"文件接收{c * 100}%");
                    }
                    byte[] recvByteLast = new byte[FileSize[i] % 1024];
                    filePackRecv.Receive(recvByteLast, recvByteLast.Length, 0);
                    //string tempfileName = Path.Combine(@"D:\testsock", FileName[i]);
                    string tempfileName = Path.Combine(recvFolderPath, FileName[i]);
                    WriteToFile.Invoke(recvByteLast, tempfileName);
                    Console.WriteLine("文件接收100%");
                }

                //byte[] recvByte = new byte[1024];
                //temp.Receive(recvByte, recvByte.Length, 0);
                //string buildFileName = Path.Combine(@"d:\", FileName[i]);
                //根据文件大小计算要切割的包大小
                //WriteToFile.Invoke(recvByte, buildFileName);//调用委托
                //recvByteList.Add(recvByte);
                //float c = (float)i / (float)int.Parse(recvStr);
                //Console.WriteLine($"文件接收{c * 100}%");
            }
            Thread.Sleep(2000);
            //filePackRecv.Close();
            Console.WriteLine("文件接收完成");
            //string sendStr = "文件接收完成";
            //byte[] bs = Encoding.ASCII.GetBytes(sendStr);
            //temp.Send(bs, bs.Length, 0);
            //temp.Close();
            s.Close();
        }
    }
}


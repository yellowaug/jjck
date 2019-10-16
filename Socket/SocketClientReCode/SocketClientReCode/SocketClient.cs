using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SocketClientReCode
{
    public class SocketClient
    {
        private int Port { get; set; }
        private string ServerHost { get; set; }
        public SocketClient() { }
        public SocketClient(int port, string serverhost)
        {
            this.Port = port;
            this.ServerHost = serverhost;
        }
        public void ClientAction(Action<byte[],string> WriteToFileAction,string FolderFullPath)
        {
            IPAddress ip = IPAddress.Parse(this.ServerHost);
            IPEndPoint iPEnd = new IPEndPoint(ip, this.Port);
            Socket recvFileCount = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("正在连接到服务端。。");
            recvFileCount.Connect(iPEnd);
            string recvStr = "";
            byte[] recvPackLength = new byte[1024];
            int packsCount = recvFileCount.Receive(recvPackLength, recvPackLength.Length, 0);
            recvStr += new ASCIIEncoding().GetString(recvPackLength, 0, packsCount);
            Console.WriteLine("客户端要接收的文件个数：{0}", recvStr);
            recvFileCount.Close();

            List<string> FileSizeList = new List<string>();
            List<string> FileNameList = new List<string>();
            for (int i = 0; i < int.Parse(recvStr)*2; i++)
            {
                Socket recvFileName = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("正在连接到服务端。。");
                recvFileName.Connect(iPEnd);
                string recvName = "";
                byte[] recvPackFileName = new byte[1024];
                packsCount = recvFileName.Receive(recvPackFileName, recvPackFileName.Length, 0);
                recvName += new ASCIIEncoding().GetString(recvPackFileName, 0, packsCount);
                //Console.WriteLine("客户端要接收的文件：{0}", recvName);
                //Console.WriteLine(i);
                //Console.WriteLine(i%2);
                if (i % 2 == 1)
                {
                    FileSizeList.Add(recvName);
                    Console.WriteLine("客户端要接收的文件大小：{0}", recvName);
                }
                else
                {
                    FileNameList.Add(recvName);
                    Console.WriteLine("客户端要接收的文件名：{0}", recvName);
                }
                recvFileName.Close();
            }

            for (int n = 0; n < FileSizeList.Count; n++)
            //foreach (var item in FileSizeList)
            {
                Console.WriteLine("文件大小为{0}",long.Parse(FileSizeList[n]));
                int filesizeCount =(int)long.Parse(FileSizeList[n]) % 65536;
                Console.WriteLine("余数为{0}",filesizeCount);
                string recvfilePath = Path.Combine(FolderFullPath, FileNameList[n]);
                if (filesizeCount == 0)
                {

                    for (int i = 0; i < long.Parse(FileSizeList[n]) / 65536; i++)
                    {
                        byte[] recvPackFile = new byte[65536];
                        Socket recvFilepack = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Console.WriteLine("正在连接到服务端。。");
                        recvFilepack.Connect(iPEnd);
                        int recvLog=recvFilepack.Receive(recvPackFile, recvPackFile.Length, 0);
                        WriteToFileAction.Invoke(recvPackFile, recvfilePath);
                        if (recvLog>0)
                        {
                            Console.WriteLine("接收标识{0}",recvLog);
                            Console.WriteLine("文件接收{0}%", (i / long.Parse(FileSizeList[n])) * 100);
                        }
                        else
                        {
                            Console.WriteLine("文件接收为空");
                        }
                        recvFilepack.Close();
                    }
                }
                else if (filesizeCount > 0 && filesizeCount < 1)
                {
                    byte[] recvPackFile = new byte[int.Parse(FileSizeList[n])];
                    Socket recvFilepack = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Console.WriteLine("正在连接到服务端。。");
                    recvFilepack.Connect(iPEnd);
                    int recvLog = recvFilepack.Receive(recvPackFile, recvPackFile.Length, 0);
                    WriteToFileAction.Invoke(recvPackFile, recvfilePath);
                    if (recvLog > 0)
                    {
                        Console.WriteLine("数据包接收成功");
                    }
                    else
                    {
                        Console.WriteLine("文件接收为空");
                    }
                    recvFilepack.Close();
                }
                else if (filesizeCount != 0 && filesizeCount > 1)
                {
                    Console.WriteLine("接收数据包的循环次数{0}", long.Parse(FileSizeList[n]) / 65536);
                    //Console.WriteLine(Int64.Parse(FileSizeList[n]) % 65536);
                    for (int i = 0; i < long.Parse(FileSizeList[n]) / 65536; i++)
                    {
                        byte[] recvPackFile = new byte[65536];
                        Socket recvFilepack = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Console.WriteLine("正在连接到服务端。。");
                        recvFilepack.Connect(iPEnd);
                        int recvLog = recvFilepack.Receive(recvPackFile, recvPackFile.Length, 0);
                        WriteToFileAction.Invoke(recvPackFile, recvfilePath);
                        if (recvLog > 0)
                        {
                            Console.WriteLine("接收标识{0}", recvLog);
                            Console.WriteLine(i);
                            Console.WriteLine("文件接收{0}%", (i / long.Parse(FileSizeList[n]) / 65536) * 100);
                        }
                        else
                        {
                            Console.WriteLine("文件接收为空");
                        }
                        
                        recvFilepack.Close();
                    }
                    Console.WriteLine("接收阶段1完成,开始接收最后一个数据包");
                    byte[] recvLastPackFile = new byte[filesizeCount];
                    Socket recvLastFilepack = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Console.WriteLine("正在连接到服务端。。");
                    recvLastFilepack.Connect(iPEnd);
                    int recvLastLog = recvLastFilepack.Receive(recvLastPackFile, recvLastPackFile.Length, 0);
                    WriteToFileAction.Invoke(recvLastPackFile, recvfilePath);
                    if (recvLastLog>0)
                    {
                        Console.WriteLine("最后一个数据包接收成功");
                    }
                    recvLastFilepack.Close();
                }
            }



        }
    }
}

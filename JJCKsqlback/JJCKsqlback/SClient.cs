using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;

namespace SocketClient
{
    class SClient
    {
        public void Client(FilePack filepackInfos)
        {
            //配置Sock连接信息
            int port = 2000;
            string host = "127.0.0.1";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint iPEnd = new IPEndPoint(ip, port);
            Socket sendFileCount = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("发送文件个数中......");
            sendFileCount.Connect(iPEnd);
            byte[] packFilecount = new ASCIIEncoding().GetBytes(filepackInfos.FileName.Count.ToString());
            sendFileCount.Send(packFilecount, packFilecount.Length, 0);
            sendFileCount.Close();
            Console.WriteLine("文件个数发送完成");
            //for (int i = 0; i < filepackInfos.FileName.Count; i++)
            //{

            //    发送文件名
            //    byte[] packFileName = new ASCIIEncoding().GetBytes(filepackInfos.FileName[i]);
            //    c.Send(packFileName, packFileName.Length, 0);
            //    Thread.Sleep(500);
            //发送文件的大小信息
            //byte[] packFileSize = new ASCIIEncoding().GetBytes(filepackInfos.FileSize[i].ToString());
            //c.Send(packFileSize, packFileSize.Length, 0);
            //Thread.Sleep(500);
            //发送文件数据包
            //for (int j = 0; j < filepackInfos.FilePackList.Count; j++)
            //{
            //    c.Send(filepackInfos.FilePackList[j], filepackInfos.FilePackList[j].Length, 0);
            //    float sendPer = (float)j / (float)filepackInfos.FilePackList.Count;
            //    Console.WriteLine($"文件发送{sendPer * 100}%");
            //}
            //}
            Socket sendFileName = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("发送文件名连接中......");
            sendFileName.Connect(iPEnd);
            foreach (var itemfileName in filepackInfos.FileName)
            {
                byte[] packFileName = new ASCIIEncoding().GetBytes(itemfileName);
                sendFileName.Send(packFileName, packFileName.Length, 0);
                Thread.Sleep(1000);
            }
            Console.WriteLine("发送文件名完成");
            sendFileName.Close();
            //发送文件的大小信息
            Socket sendFileSize = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("连接中......");
            sendFileSize.Connect(iPEnd);
            foreach (var itemfileSize in filepackInfos.FileSize)
            {
                byte[] packFileSize = new ASCIIEncoding().GetBytes(itemfileSize.ToString());
                sendFileSize.Send(packFileSize, packFileSize.Length, 0);
                Thread.Sleep(1000);
            }
            Console.WriteLine("发送文件大小信息完成");
            sendFileSize.Close();
            //byte[] packCount = new ASCIIEncoding().GetBytes(fileInfos.Count.ToString());
            //c.Send(packCount, packCount.Length, 0);

            //发送文件数据包,整个数据包发出去，不做切割，然后在服务端进行切割复原。
            //for (int i = 0; i < filepackInfos.FilePackList.Count; i++)
            //{

            //    c.Send(filepackInfos.FilePackList[i], filepackInfos.FilePackList[i], 0);
            //    float sendPer = (float)i / (float)filepackInfos.FilePackList.Count;
            //    Console.WriteLine($"文件发送{sendPer * 100}%");
            //}
            Socket sendFilePack = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sendFilePack.Connect(iPEnd);
            foreach (var itemfilePack in filepackInfos.FilePackList)
            {
                Thread.Sleep(1);
                for (int i = 0; i < itemfilePack.TempFilePackList.Count; i++)
                {
                    sendFilePack.Send(itemfilePack.TempFilePackList[i], itemfilePack.TempFilePackList[i].Length, 0);
                    float sendPer = (float)i / (float)itemfilePack.TempFilePackList.Count;
                    Console.WriteLine($"文件发送{sendPer * 100}%");
                    Thread.Sleep(1);
                }

            }
            sendFilePack.Close();
            Console.WriteLine("发送数据包完成");
            //foreach (var itembyte in fileInfos)
            //{                
            //    c.Send(itembyte, itembyte.Length, SocketFlags.None);
            //}

            //string recvStr = "";
            //byte[] recvBytes = new byte[1024];
            //int bytes;
            //bytes = c.Receive(recvBytes, recvBytes.Length, 0);
            //recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
            //Console.WriteLine($"client get message{recvStr}");
            //c.Close();

        }
    }
}


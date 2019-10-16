using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketServerReCode
{
    public class SocketServer
    {
        public static int Port { get; set; }
        public static string HostIp { get; set; }
        public static int ListNum { get; set; }
        public int PackSize { get; set; }
        public SocketServer() { }
        //public SocketServer(int port,string hostip,int listnum)
        //{

        //    SocketServer.Port = port;
        //    SocketServer.HostIp = hostip;
        //    SocketServer.ListNum = listnum;
            
        //}
        /// <summary>
        /// 配置SOCKET服务器链接信息
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="hostip">IP地址</param>
        /// <param name="listnum">监听个数</param>
        /// <returns>SOCKET对象</returns>
        public static Socket ConfigSocket(int port, string hostip, int listnum)
        {
            //SocketServer.Port = port;
            //SocketServer.HostIp = hostip;
            //SocketServer.ListNum = listnum;
            IPAddress ip = IPAddress.Parse(hostip);
            IPEndPoint iPEnd = new IPEndPoint(ip, port);
            Socket socketobj = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketobj.SendTimeout = 0;
            Console.WriteLine("等待客户端进行连接");
            socketobj.Bind(iPEnd);
            socketobj.Listen(listnum);
            
            return socketobj;
        }
        /// <summary>
        /// socket服务端发送数据包
        /// </summary>
        /// <param name="sockobj">socket链接对象</param>
        /// <param name="datapack">数据包</param>
        public void SendPack(Socket sockobj,byte[] datapack)
        {
            Socket tempSend = sockobj.Accept();
            int sendCount=tempSend.Send(datapack, datapack.Length, SocketFlags.None);
            if (sendCount!=0)
            {
                Console.WriteLine("数据包发送成功");
            }
            else if (sendCount==0)
            {
                Console.WriteLine("数据包发送失败");
            }
            tempSend.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace FTPClient
{
    public class FtpAction
    {
        private int buffSize = 65536;
        private string FtpUrl { get; set; }
        private string FtpUserName { get; set; }
        private string FtpPassWord { get; set; }
        public FtpAction(string url,string username,string password)
        {
            this.FtpUrl = url;
            this.FtpUserName = username;
            this.FtpPassWord = password;
        }
        public void GetFtpDirectory()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(this.FtpUrl);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            
            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(this.FtpUserName, this.FtpPassWord);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
           
            //Console.WriteLine(reader.Read());
            Console.WriteLine(reader.ReadLine());
            Console.WriteLine("===========================");
            var s = reader.ReadToEnd().ToString();
            Console.WriteLine(s);   

            Console.WriteLine($"Directory List Complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }
        public void FtpDownLocal(string localFile)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(this.FtpUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(this.FtpUserName, this.FtpPassWord);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            FileStream localfileStream = new FileStream(localFile, FileMode.Create);
            byte[] byteBuffer = new byte[buffSize];
            int bytescache = responseStream.Read(byteBuffer, 0, buffSize);
            try
            {
                while (bytescache > 0)
                {
                    localfileStream.Write(byteBuffer, 0, bytescache);
                    bytescache = responseStream.Read(byteBuffer, 0, buffSize);
                    Console.WriteLine("接收到的缓存字节数为{0}",bytescache);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                response.Close();
                responseStream.Close();
                localfileStream.Close();
               
            }
            //StreamReader reader = new StreamReader(responseStream);
        }
    }
}

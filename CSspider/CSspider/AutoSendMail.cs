using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace CSspider
{
    class AutoSendMail
    {
        public Boolean Mailbox(string name, string content)
        {
            MailMessage message = new MailMessage();
            //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
            System.Net.Mail.MailAddress fromAddr = new System.Net.Mail.MailAddress("2363402230@qq.com", "我的网站");
            message.From = fromAddr;

            //设置收件人,可添加多个,添加方法与下面的一样
            message.To.Add(name);

            //设置抄送人
            message.CC.Add("2363402230@qq.com");

            //设置邮件标题
            message.Subject = "获取验证码";

            //设置邮件内容
            message.Body = content;

            //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
            SmtpClient client =  new SmtpClient("smtp.qq.com", 25);

            //设置发送人的邮箱账号和密码，POP3/SMTP服务要开启, 密码要是POP3/SMTP等服务的授权码
            client.Credentials = new System.Net.NetworkCredential("2363402230@qq.com", "fhszmpegwoqnecja");//vtirsfsthwuadjfe  fhszmpegwoqnecja

            //启用ssl,也就是安全发送
            client.EnableSsl = true;

            //发送邮件
            client.Send(message);
            return true;
        }
    }
}

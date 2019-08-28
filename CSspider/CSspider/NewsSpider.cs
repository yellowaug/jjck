using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;

namespace CSspider
{
    interface IGetNewsPage
    {
        void GetPageHtml(string urlpath);
    }
    class NewsSpider : IGetNewsPage
    {
        /// <summary>
        /// 获取环球网产经新闻的标题以及时间，链接
        /// </summary>
        void IGetNewsPage.GetPageHtml(string urlpath)
        {
            var url = @"http://finance.huanqiu.com/"+urlpath+"/";
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(url);
            var statusCode = web.StatusCode;           
            if (statusCode==HttpStatusCode.OK)
            {
                var node = doc.DocumentNode.SelectNodes("//ul[@class='listPicBox']//h3/a").ToList();
                var newstime = doc.DocumentNode.SelectNodes("//ul[@class='listPicBox']//h6").ToList();
                for (int i = 0; i < node.Count; i++)
                {
                    var title = node[i].Attributes[1].Value;
                    var newsUrl= node[i].Attributes[0].Value;
                    var newsTime = newstime[i].InnerText;
                    Console.WriteLine($"标题：{title}\n新闻链接：{newsUrl}\n新闻时间：{newsTime}");
                    Console.WriteLine("======================================");
                    //还有写入数据库的功能要完成
                }
                
            }
            else
            {
                Console.WriteLine($"网络链接异常错误代码：{web.StatusCode}");
            }
            
        }
    }
}

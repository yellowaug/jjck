using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSspider
{
    public class SZJYProject
    {
        public int Stock_code { get; set; }

    }
    interface IGetNewsPage
    {
        void GetHuanqiuHtml(string urlpath);
        
    }
    interface IGetSZJY
    {
        void GetSZJYHtml(string url);
    }
    class NewsSpider : IGetNewsPage,IGetSZJY
    {
        /// <summary>
        /// 获取环球网产经新闻的标题以及时间，链接
        /// </summary>
        void IGetNewsPage.GetHuanqiuHtml(string urlpath)
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

        void IGetSZJY.GetSZJYHtml(string url)
        {
            HttpClient http = new HttpClient();
            var respone = http.GetStringAsync(url).Result;
            string pattern = @"{stock_code.*?}";
            var matches = Regex.Matches(respone, pattern);
            foreach (var item in matches)
            {
                var a =JsonConvert.DeserializeObject<SZJYProject>(item.ToString());
                Console.WriteLine(a.Stock_code);
                //Console.WriteLine(a);
            }
            
        }
    }
}

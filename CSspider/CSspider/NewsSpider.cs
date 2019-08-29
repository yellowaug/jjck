using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using DataAccess.Entity;
using DataAccess.EntityFramework;


namespace CSspider
{
    /// <summary>
    /// 这个类是熟悉类，缓存爬虫获取到的字段信息
    /// </summary>
    public class SZJYProject
    {
        public string Stock_code { get; set; }
        public string Bulletin_date { get; set; }
        public string Bulletin_title { get; set; }
        public string Bulletin_file_url { get; set; }

    }
    /// <summary>
    /// 这里是接口
    /// </summary>
    interface IGetNewsPage
    {
        void GetHuanqiuHtml(string urlpath);
        
    }
    interface IGetSZJY
    {
        void GetSZJYHtml(string stockcode);
    }
    /// <summary>
    /// 接口的实现
    /// IGetNewsPage获取环球网产经新闻的标题以及时间，链接
    /// IGetSZJY根据股票代码获取股票代码的新闻公告
    /// </summary>
    class NewsSpider : IGetNewsPage,IGetSZJY
    {
        private New huanqiuNews = new New();
        private BaseDbContext db = new BaseDbContext();
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
                    huanqiuNews.Tite = title;
                    huanqiuNews.ContentUri = newsUrl;
                    huanqiuNews.ReleaseTime = DateTime.Parse(newsTime);
                }
                db.Database.EnsureCreated();
                db.News.Add(huanqiuNews);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"网络链接异常错误代码：{web.StatusCode}");
            }            
        }

        void IGetSZJY.GetSZJYHtml(string stockcode)
        {
            HttpClient http = new HttpClient();
            string url = @"http://www.sse.com.cn/js/common/stocks/new/"+stockcode+".js";
            var respone = http.GetStringAsync(url).Result;
            string pattern = @"{stock_code.*?}";
            var matches = Regex.Matches(respone, pattern);
            foreach (var item in matches)
            {
                var paresResult =JsonConvert.DeserializeObject<SZJYProject>(item.ToString());
                Console.WriteLine($"股票代码：{paresResult.Stock_code}\n" +
                                    $"新闻标题：{paresResult.Bulletin_title}\n" +
                                    $"新闻PDF：{paresResult.Bulletin_file_url}" +
                                    $"\n发布时间：{paresResult.Bulletin_date}");
                Console.WriteLine("============================================");
                //Console.WriteLine(a);
            }
            
        }
    }
}

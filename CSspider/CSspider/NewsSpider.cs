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
        List<New> GetHuanqiuHtml(string urlpath);
        
    }
    interface IGetSZJY
    {
        List<New> GetSZJYHtml(string stockcode);
    }
    /// <summary>
    /// 写入数据库的接口
    /// </summary>
    interface IupLoadSql
    {
        void UptoSql(List<New> newsList);
    }

    /// <summary>
    /// 接口的实现
    /// IGetNewsPage获取环球网产经新闻的标题以及时间，链接
    /// IGetSZJY根据股票代码获取股票代码的新闻公告
    /// </summary>
    class NewsSpider : IGetNewsPage,IGetSZJY
    {
        
        //private BaseDbContext db = new BaseDbContext();
        List<New> IGetNewsPage.GetHuanqiuHtml(string urlpath)
        {
            List<New> newsList = new List<New>();
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
                    New huanqiuNews = new New();                   
                    huanqiuNews.Tite = title;
                    huanqiuNews.ContentUri = newsUrl;
                    huanqiuNews.ReleaseTime = DateTime.Parse(newsTime);
                    newsList.Add(huanqiuNews);
                    //db.News.Add(huanqiuNews);
                }
                //db.Database.EnsureCreated();
                return newsList;
                
            }
            else
            {
                Console.WriteLine($"网络链接异常错误代码：{web.StatusCode}");
                return null ;
            }            
        }

        List<New> IGetSZJY.GetSZJYHtml(string stockcode)
        {
            List<New> newsSSEList = new List<New>();
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
                New sseNews = new New();
                sseNews.Stock = new Stock { Code = paresResult.Stock_code };
                sseNews.Tite = paresResult.Bulletin_title;
                sseNews.ContentUri = @"http://www.sse.com.cn" + paresResult.Bulletin_file_url;
                sseNews.ReleaseTime = DateTime.Parse(paresResult.Bulletin_date);
                newsSSEList.Add(sseNews);
                //db.News.Add(sseNews);
                //Console.WriteLine(a);
            }
            return newsSSEList;
            //db.SaveChanges();

        }
    }
    /// <summary>
    /// 这个类是关于数据库操作的类
    /// </summary>
    class SqlContron : IupLoadSql
    {
        private BaseDbContext db = new BaseDbContext();



        /// <summary>
        /// 向数据库传入数据
        /// </summary>
        /// <param name="newsList"></param>
        void IupLoadSql.UptoSql(List<New> newsList)
        {
            DateTime todaytime = DateTime.Now;                      
            var timestring=todaytime.ToString("yyyy-MM-dd");
            foreach (var item in newsList)
            {                              
                var newstimestring = item.ReleaseTime.ToString("yyyy-MM-dd");
                int code = String.Compare(timestring, newstimestring);
                Console.WriteLine($"P{code}");
                //db.Database.EnsureCreated(); //如果是数据库发生变更，放开这行
                int existCode=db.News.Where(nlist => nlist.Tite == item.Tite).Count();
                if (code==0&&existCode==0)
                {
                    
                    db.News.Add(item);
                    Console.WriteLine("当天的新闻写入成功");
                }
                else
                {

                    Console.WriteLine("该新闻是昨天的新闻");
                }
                
            }
            db.SaveChanges();
        }
    }
}

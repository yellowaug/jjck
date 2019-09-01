using DataAccess.Entity;
using DataAccess.EntityFramework;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace CSspider
{
    //public class DataTemp
    //{
    //    public string CompanyName { get; set; }
    //    public string StockCode { get; set; }
    //    public string ExsitDate { get; set; }
    //    public string PayDate { get; set; }
    //    public string Dividend { get; set; }
    //    public string RateOfReturn { get; set; }
    //}
    interface IDividendSpider
    {
        List<Dividend> GetDividendReport(string startDate,string endDate);
    }
    interface IInserIntoSql
    {
        void InserIntoSql(List<Dividend> temps);
    }
    public class DividendSpider : IDividendSpider
    {
        #region HRZ的代码
        //public List<DataTemp> Get()
        //{
        //    List<DataTemp> dataTemps = new List<DataTemp>();
        //    //请求数据
        //    HttpClient httpClient = new HttpClient();
        //    var HttpContent = new FormUrlEncodedContent(new Dictionary<string, string>
        //            {
        //                { "country[]", "37"},
        //                { "dateFrom", "2019-08-1"},
        //                { "dateTo", "2019-08-31"},
        //                { "currentTab", "custom"},
        //                { "limit_from", "0"}
        //            });
        //    httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
        //    httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
        //    var httpesponseMessage = httpClient.PostAsync("https://cn.investing.com/dividends-calendar/Service/getCalendarFilteredData", HttpContent).Result;
        //    var  jsonStr =   httpesponseMessage.Content.ReadAsStringAsync().Result;
        //   var jsonOb = JsonConvert.DeserializeObject<dynamic>(jsonStr);
        //   var xml = jsonOb["data"];

        //    //解析xml
        //    var htmlDoc = new HtmlDocument();
        //    htmlDoc.LoadHtml(xml.ToString());
        //    var companyName = htmlDoc.DocumentNode.SelectNodes("//td[@class='left noWrap']");
        //    var publicDate= htmlDoc.DocumentNode.SelectNodes("//td[3]");
        //    var dividend = htmlDoc.DocumentNode.SelectNodes("//td[4]");
        //    var endDate= htmlDoc.DocumentNode.SelectNodes("//td[6]");
        //    var rateOfReturn= htmlDoc.DocumentNode.SelectNodes("//td[7]");
        //    for (int i = 0; i < companyName.Count; i++)
        //    {
        //        var stockCode = Regex.Match(companyName[i].InnerText, @"\d{6}");
        //        dataTemps.Add(new DataTemp
        //        {
        //            CompanyName = companyName[i].Attributes["title"].Value,
        //            StockCode = stockCode.Value,
        //            ExsitDate = publicDate[i].InnerText,
        //            Dividend = dividend[i].InnerText,              
        //            PayDate = endDate[i].InnerText,
        //            RateOfReturn = rateOfReturn[i].InnerText
        //        });
        //        Console.WriteLine("==================================");
        //        Console.WriteLine($"公司名称：{companyName[i].Attributes["title"].Value}," +
        //            $"\n股票代码：{stockCode.Value},\n除息日：{publicDate[i].InnerText}," +
        //            $"\n股息：{dividend[i].InnerText}，\n付息日：{endDate[i].InnerText}，" +
        //            $"\n收益率：{rateOfReturn[i].InnerText}。");

        //    }
        //    return dataTemps;

        //====================================================================
        //上半部分是我修改以后的实现，已基本实现功能，然后我就封装成了接口的形式
        //====================================================================
        //var trs = table.Except(ths).ToList();
        //var ths = htmlDoc.DocumentNode.SelectNodes("//tr[@tablesorterdivider]").ToList();
        //var nodeinfo = htmlDoc.DocumentNode.SelectNodes("//tr");
        //保存到数据库
        //BaseDbContext baseDbContext = new BaseDbContext();
        //for (int i = 0; i < trs.Count(); i++)
        //{
        //    var item = trs[i];

        //    Dividend dividend = new Dividend();
        //    Stock stock = new Stock();
        //    var test = item.SelectNodes("//span[@class='earnCalCompanyName middle']");
        //    //stock.Name = test;
        //    //stock.Name = item.SelectNodes("//span[@class='earnCalCompanyName middle']").First().InnerText;
        //    stock.Code = item.SelectNodes("//a").FirstOrDefault().InnerText;
        //    dividend.DividendDate = DateTime.Parse(item.SelectNodes("//td")[3].InnerText);
        //    dividend.Amount = decimal.Parse(item.SelectNodes("//td")[4].InnerText);
        //    dividend.RateOfReturn = item.SelectNodes("//td")[7].InnerText;
        //    dividend.Stock = stock;
        //    baseDbContext.Add(dividend);
        //}


        //保存到数据库
        //BaseDbContext baseDbContext = new BaseDbContext();
        //foreach (var item in trs)
        //{
        //    Dividend dividend = new Dividend();
        //    Stock stock = new Stock();
        //    stock.Name = item.SelectNodes("//span[@class='earnCalCompanyName middle']").FirstOrDefault().InnerText;
        //    stock.Code = item.SelectNodes("//a").FirstOrDefault().InnerText;
        //    dividend.DividendDate = DateTime.Parse(item.SelectNodes("//td")[3].InnerText);
        //    dividend.Amount = decimal.Parse(item.SelectNodes("//td")[4].InnerText);
        //    dividend.RateOfReturn = item.SelectNodes("//td")[7].InnerText;
        //    dividend.Stock = stock;
        //    baseDbContext.Add(dividend);
        //}
        // baseDbContext.SaveChanges();
        #endregion
        /// <summary>
        /// 获取英为财情的分红报告
        /// </summary>
        /// <param name="startDate">传入的时间格式应为：2018-08-01</param>
        /// <param name="endDate">传入的时间格式应为：2018-08-01</param>
        /// <returns>返回一个列表属性</returns>
        List<Dividend> IDividendSpider.GetDividendReport(string startDate, string endDate) 
        {
            List<Dividend> dataTemps = new List<Dividend>();
            //请求数据
            HttpClient httpClient = new HttpClient();
            var HttpContent = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            { "country[]", "37"},
                            { "dateFrom", startDate},
                            { "dateTo", endDate},
                            { "currentTab", "custom"},
                            { "limit_from", "0"}
                        });
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            var httpesponseMessage = httpClient.PostAsync("https://cn.investing.com/dividends-calendar/Service/getCalendarFilteredData", HttpContent).Result;
            var jsonStr = httpesponseMessage.Content.ReadAsStringAsync().Result;
            var jsonOb = JsonConvert.DeserializeObject<dynamic>(jsonStr);
            var xml = jsonOb["data"];

            //解析xml
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(xml.ToString());
            var companyName = htmlDoc.DocumentNode.SelectNodes("//td[@class='left noWrap']");
            var publicDate = htmlDoc.DocumentNode.SelectNodes("//td[3]");
            var dividend = htmlDoc.DocumentNode.SelectNodes("//td[4]");
            var payDate = htmlDoc.DocumentNode.SelectNodes("//td[6]");
            var rateOfReturn = htmlDoc.DocumentNode.SelectNodes("//td[7]");
            for (int i = 0; i < companyName.Count; i++)
            {
                var stockCode = Regex.Match(companyName[i].InnerText, @"\d{6}");
                dataTemps.Add(new Dividend
                {
                    Stock = new Stock { Name = companyName[i].Attributes["title"].Value, Code = stockCode.Value },
                    RegistrationTime = DateTime.Parse(publicDate[i].InnerText), 
                    Amount = decimal.Parse(dividend[i].InnerText),
                    DividendDate = DateTime.Parse(payDate[i].InnerText),
                    RateOfReturn = rateOfReturn[i].InnerText
                }); //还有一个releasetime这个字段不知道填什么进去。。
                Console.WriteLine("==================================");
                Console.WriteLine($"公司名称：{companyName[i].Attributes["title"].Value}," +
                    $"\n股票代码：{stockCode.Value},\n除息日：{publicDate[i].InnerText}," +
                    $"\n股息：{dividend[i].InnerText}，\n付息日：{payDate[i].InnerText}，" +
                    $"\n收益率：{rateOfReturn[i].InnerText}。");
            }
            return dataTemps;
            
        }
    }
    public class UpLoadSql : IInserIntoSql
    {
        void IInserIntoSql.InserIntoSql(List<Dividend> temps)
        {
            BaseDbContext db = new BaseDbContext();

            foreach (var item in temps)
            {
                var DataExsit = db.Dividends.Where(x => x.Stock.Name == item.Stock.Name).Count(); //判断爬取到的数据是否存在
                Console.WriteLine("存在标签：{0}",DataExsit);
                if (DataExsit==0)
                {
                    db.Dividends.Add(item);
                }
                else
                {
                    Console.WriteLine("爬取到的数据已存在，略过");
                }
            }
            db.SaveChanges();
        }
    }
}

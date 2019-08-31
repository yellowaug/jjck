using DataAccess.Entity;
using DataAccess.EntityFramework;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CSspider
{
    public class DividendSpider
    {
        public void Get()
        {

            //请求数据
            HttpClient httpClient = new HttpClient();
            var HttpContent = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "country[]", "37"},
                        { "dateFrom", "2019-08-1"},
                        { "dateTo", "2019-08-31"},
                        { "currentTab", "custom"},
                        { "limit_from", "0"}
                    });
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            var httpesponseMessage = httpClient.PostAsync("https://cn.investing.com/dividends-calendar/Service/getCalendarFilteredData", HttpContent).Result;
            var  jsonStr =   httpesponseMessage.Content.ReadAsStringAsync().Result;
           var jsonOb = JsonConvert.DeserializeObject<dynamic>(jsonStr);
           var xml = jsonOb["data"];

            //解析xml
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(xml.ToString());
            var companyName = htmlDoc.DocumentNode.SelectNodes("//td[@class='left noWrap']");
            var publicDate= htmlDoc.DocumentNode.SelectNodes("//td[3]");
            var dividend = htmlDoc.DocumentNode.SelectNodes("//td[4]");
            var endDate= htmlDoc.DocumentNode.SelectNodes("//td[6]");
            var rateOfReturn= htmlDoc.DocumentNode.SelectNodes("//td[7]");
            for (int i = 0; i < companyName.Count; i++)
            {
                Console.WriteLine("==================================");
                Console.WriteLine($"公司名称：{companyName[i].Attributes["title"].Value},\n除息日：{publicDate[i].InnerText},\n股息：{dividend[i].InnerText}，\n付息日：{endDate[i].InnerText}，\n收益率：{rateOfReturn[i].InnerText}。");
            }





            #region HRZ的代码
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

        }
    }
}

using DataAccess.Entity;
using DataAccess.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CSspider
{

    /// <summary>
    /// 行情，分红期间股票价格
    /// </summary>
    public class QuoteSpider
    {

        /// <summary>
        /// 获取分红期间价格
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="dividendDate"></param>
        public void Get(string stockCode,DateTime dividendDate)
        {

            //获取价格数据
            var url = "http://q.stock.sohu.com/hisHq?code=cn_" + stockCode + $"&start={dividendDate.AddDays(-6).ToString("yyyyMMdd")}&end={dividendDate.Date.ToString("yyyyMMdd")}";
            HttpClient httpClient = new HttpClient();
           var jsonStr = httpClient.GetStringAsync(url).Result;
           var jsonObj=   JsonConvert.DeserializeObject<dynamic>(jsonStr);
           jsonObj = jsonObj[0].hq;


            BaseDbContext baseDbContext = new BaseDbContext();

            //创建股票
            Stock stock = new Stock { Code = stockCode };
            baseDbContext.Stocks.Add(stock);
            baseDbContext.SaveChanges();

            //关联价格
            foreach (var item in jsonObj)
            {
                Quote quote = new Quote();
                quote.Date = item[0];
                quote.StartPrice = item[1];
                quote.EndPrice = item[2];
                quote.MinPrice = item[5];
                quote.MaxPrice = item[6];
                quote.Volume = item[7];
                quote.AMO = item[8];
                quote.TurnoverRate = item[9];
                baseDbContext.Quotes.Add(quote);
            }
            baseDbContext.SaveChanges();
        }



    }
}

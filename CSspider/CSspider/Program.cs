using System;

namespace CSspider
{
    /// <summary>
    /// 使用到的爬虫包HtmlAgilityPack
    /// 该包的API文档地址:https://html-agility-pack.net/select-nodes
    /// Xpath语法：https://www.runoob.com/xpath/xpath-syntax.html
    /// 包安装方式:Install-Package HtmlAgilityPack -Version 1.11.12
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IupLoadSql loadSql = new SqlContron();
            IGetNewsPage newsPage = new NewsSpider();
            var huanqiu= newsPage.GetHuanqiuHtml("chanjing");
            loadSql.UptoSql(huanqiu);
            var jinrong=newsPage.GetHuanqiuHtml("jinr");
            loadSql.UptoSql(jinrong);
            IGetSZJY getSZJY = new NewsSpider();
            var qdg=getSZJY.GetSZJYHtml("601298");
            loadSql.UptoSql(qdg);
            //getSZJY.GetSZJYHtml("900929");
            DividendSpider spider = new DividendSpider();
            spider.Get();
            Console.ReadKey();
        }
    }
}

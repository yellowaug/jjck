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
            IGetNewsPage newsPage = new NewsSpider();
            newsPage.GetHuanqiuHtml("chanjing");
            newsPage.GetHuanqiuHtml("jinr");
            IGetSZJY getSZJY = new NewsSpider();
            getSZJY.GetSZJYHtml("601298");
            getSZJY.GetSZJYHtml("900929");
            Console.ReadKey();
        }
    }
}

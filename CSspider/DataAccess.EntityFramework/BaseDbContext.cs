using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework
{
    public class BaseDbContext : DbContext
    {
      //  public BaseDbContext(DbContextOptions options)
      //: base(options) {}

        public BaseDbContext()
     : base() {}


        /// <summary>
        /// 新闻表
        /// </summary>
        public DbSet<New> News { get; set; }

        /// <summary>
        /// 股票表
        /// </summary>
        public DbSet<Stock> Stocks { get; set; }


        /// <summary>
        /// 分红表
        /// </summary>
        public DbSet<Dividend>  Dividends { get; set; }

        /// <summary>
        /// 行情表（价格表）
        /// </summary>
        public DbSet<Quote>   Quotes { get; set; }





    //创建日志工厂
    private static ILoggerFactory Mlogger => new LoggerFactory()
                 .AddDebug((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name))
                .AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory =
            optionsBuilder
                .UseLoggerFactory(Mlogger); //注入日志工厂


            optionsBuilder.UseSqlServer(@"Data Source=10.12.2.6; Initial Catalog=HrzTest; Uid=sa; Pwd=Jingjia@2@20; App=EntityFramework;");
        }
    }
}

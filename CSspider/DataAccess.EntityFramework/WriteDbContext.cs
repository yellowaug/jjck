using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace DataAccess.EntityFramework
{
    /// <summary>
    /// CUD上下文
    /// </summary>
    public class WriteDbContext: BaseDbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options)
        : base(options) { }



        //创建日志工厂
        private static ILoggerFactory Mlogger => new LoggerFactory()
                 .AddDebug((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name))
                .AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory =
            optionsBuilder
                .UseLoggerFactory(Mlogger); //注入日志工厂
               
        }

    }
}

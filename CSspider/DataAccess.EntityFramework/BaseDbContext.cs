using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options)
      : base(options) {}

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

        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    //新增
                    case EntityState.Added:
                        entry.CurrentValues["ID"] = Guid.NewGuid();
                        entry.CurrentValues["CreateDateTime"] = DateTime.Now;    //创建时间
                        break;

                    //删除
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        entry.CurrentValues["IsDelete"] = true;                  //软删除
                        entry.CurrentValues["UpdateDateTime"] = DateTime.Now;    //更新时间
                        break;


                    //修改
                    case EntityState.Modified:
                        entry.Property("CreateDateTime").IsModified = false;    //创建时间不允许修改
                        entry.CurrentValues["UpdateDateTime"] = DateTime.Now;   //更新时间
                        break;

                }
            }

            return base.SaveChanges();
        }
    }
}

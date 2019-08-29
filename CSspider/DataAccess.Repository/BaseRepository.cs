using DataAccess.Entity;
using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccess.Repository
{
    public  class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>, new()
    {

        /// <summary>
        /// 读上下文
        /// </summary>
        public ReadDbContext ReadDbContext { get; }

        /// <summary>
        /// 写上下文
        /// </summary>
        public WriteDbContext WriteDbContext { get; }

        public BaseRepository(ReadDbContext readDbContext, WriteDbContext writeDbContext)
        {
            ReadDbContext = readDbContext;
            WriteDbContext = writeDbContext;
        }
        public int Count()
        {
           return  ReadDbContext.Set<TEntity>().Count();
        }

        /// <summary>
        /// 无查询删除
        /// </summary>
        /// <param name="id">删除的实体对象ID</param>
        public void Delete(TPrimaryKey id)
        {
            TEntity entity = new TEntity { ID = id };
            WriteDbContext.Entry(entity).State = EntityState.Deleted;
            WriteDbContext.SaveChanges();   
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            WriteDbContext.Update(entity);
            WriteDbContext.SaveChanges();
        }

        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            WriteDbContext.Add(entity);
            WriteDbContext.SaveChanges();
        }
    }
}

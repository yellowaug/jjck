using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    /// <summary>
    /// 基类仓储
    /// </summary>
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>, new()
    {

        int Count();

        
        void Delete(TPrimaryKey id);


        void Update(TEntity entity);

        void Add(TEntity entity);

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entity
{
    /// <summary>
    /// 股票
    /// </summary>
    public class Stock  :BaseEntity
    {

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}

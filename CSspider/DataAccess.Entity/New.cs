using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entity
{
    /// <summary>
    /// 新闻
    /// </summary>
    public class New :BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Tite { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseTime { get; set; }

        /// <summary>
        /// 内容链接
        /// </summary>
        public string ContentUri { get; set; }

        /// <summary>
        /// 股票
        /// </summary>
        public Stock Stock { get; set; }
    }
}

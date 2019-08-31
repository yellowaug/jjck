using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entity
{
   /// <summary>
   /// 每日行情
   /// </summary>
        
    public class Quote : BaseEntity
    {
        /// <summary>
        /// 开盘价
        /// </summary>
        public decimal StartPrice { get; set; }

        /// <summary>
        /// 收盘价
        /// </summary>
        public decimal EndPrice { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// 最低价
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 成交量(手)
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// 成交金额(万)
        /// </summary>
        public decimal AMO { get; set; }

        /// <summary>
        /// 换手率
        /// </summary>
        public string TurnoverRate { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entity
{

    /// <summary>
    /// 分红
    /// </summary>
    public class Dividend : BaseEntity
    {

        /// <summary>
        /// 公告发布时间
        /// </summary>
        public DateTime ReleaseTime { get; set; }

        /// <summary>
        /// 股权登记日期
        /// </summary>
        public DateTime RegistrationTime { get; set; }

        /// <summary>
        /// 除权日、分红日
        /// </summary>
        public DateTime DividendDay { get; set; } 
        
        
        /// <summary>
        /// 每股分红金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 股票
        /// </summary>
        public Stock  Stock { get; set; }

       
    }
}

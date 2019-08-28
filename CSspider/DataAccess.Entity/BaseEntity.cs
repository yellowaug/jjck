using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entity
{
    public  abstract class BaseEntity<T>
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        /// 
        [Key]
        public T ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }


        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }


        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
}
}

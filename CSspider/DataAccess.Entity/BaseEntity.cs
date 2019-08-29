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
        [Key]
        public T ID { get; set; }

    }
}

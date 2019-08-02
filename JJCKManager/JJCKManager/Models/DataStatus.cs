using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJCKManager.Models
{
    [Table("DataStatus",Schema ="JJCK")]
    public class DataStatus
    {
        [Key]
        public int DasId { get; set; }
        [Required(ErrorMessage ="数据状态码不允许为空")]
        public int statuscode { get; set; }
        [Required(ErrorMessage ="数据状态描述不允许为空")]
        [StringLength(15,ErrorMessage ="状态描述不能超过15个字符")]
        public string statusdesc { get; set; }

    }
}
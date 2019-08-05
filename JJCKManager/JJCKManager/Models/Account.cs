using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJCKManager.Models
{
    [Table("Account",Schema = "JJCK")]
    public class Account
    {
        [Key]
        public int Uid { get; set; }
        [Required(ErrorMessage ="用户名不允许为空")]
        [MinLength(3,ErrorMessage ="用户名不允许低于3个字符")]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码不允许为空")]
        [MinLength(5, ErrorMessage = "密码不允许低于5个字符")]
        [Display(Name ="密码")]
        public string PassWord { get; set; }
        [Required(ErrorMessage ="注册时间不允许为空")]
        [Display(Name ="注册时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime Createdate { get; set; }
        public int ?DaId { get; set; }
        public virtual Datastatus Datastatus { get; set; }
        public int ?AccId { get; set; }
        public virtual AccStatus Accstatus { get; set; }

    }
}
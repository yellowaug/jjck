using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace JJCKManager.Models
{
    [Table("OtherAccountName",Schema ="JJCK")]
    public class OtherAccount
    {
        [Key]
        public int OAccID { get; set; }
        [Required(ErrorMessage ="账号名称不允许为空")]
        [StringLength(20,ErrorMessage ="账号名称不能超过20个字符")]
        [Display(Name ="账号")]
        public string OtherAccountName { get; set; }
        [Required(ErrorMessage = "账号密码不允许为空")]
        [Display(Name = "密码")]
        public string PassWord { get; set; }
        [Display(Name = "账号用途")]
        public string AccountDesc { get; set; }
        [Display(Name ="创建人")]
        [ForeignKey("accountUser")]
        public int Creater { get; set; }
        public Account accountUser { get; set; }
        [Display(Name ="是否删除")]
        public int DaId { get; set; }
        public virtual Datastatus Datastatus { get; set; }
    }
}
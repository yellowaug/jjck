using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJCKManager.Models
{
    [Table("WebManagerAccount",Schema = "JJCK")]
    public class WebManagerAccount
    {
        [Key]
        public int WaID { get; set; }
        [Required(ErrorMessage ="账号信息不允许为空")]
        [Display(Name ="Web账号")]
        public string AccountName { get; set; }
        [Required(ErrorMessage ="账号密码不允许为空")]
        [Display(Name ="Web账号密码")]
        public string AccountPassWord { get; set; }
        [Required(ErrorMessage = "登录地址不允许为空")]
        [Display(Name = "登录地址")]
        public string WebUrlORIPaddress { get; set; }
        [Required(ErrorMessage = "账号描述不允许为空")]
        [Display(Name = "Web账号描述")]
        public string WebAccountDesc { get; set; }
        [Display(Name ="创建时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }
        [Display(Name = "创建人")]
        [ForeignKey("AccountUser")]
        public int CreateUser { get; set; }
        public virtual Account AccountUser { get; set; }
    }
}
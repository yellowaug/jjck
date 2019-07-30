using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJCKManager.Models
{
    [Table("VmHostAccount",Schema = "JJCK")]
    public class VMHostAccount
    {
        [Key]
        public int VmhostId { get; set; }
        [Required(ErrorMessage ="虚拟机账号不允许为空")]
        [Display(Name ="VM账号")]
        public string VMhostName{ get; set; }
        [Required(ErrorMessage = "虚拟机登录IP不允许为空")]
        [Display(Name = "VM登录IP")]
        public string VMLoginIp { get; set; }
        [Required(ErrorMessage = "虚拟机密码不允许为空")]
        [Display(Name = "VM登录密码")]
        public string VMLoginPassWord { get; set; }
        [Required(ErrorMessage = "虚拟机账号创建时间不允许为空")]
        [Display(Name = "录入时间")]
        public DateTime VMCreateTime { get; set; }
        [Display(Name = "录入人")]
        public string CreateUser { get; set; }
        [Display(Name = "VM账号信息描述")]
        public string VmAccountDesc { get; set; }
        public Account acc { get; set; }
    }
}
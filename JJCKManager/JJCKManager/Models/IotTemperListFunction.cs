using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJCKManager.Models
{
    //这个表在web上面只能读不能写
    [Table("TempFunction",Schema = "JJCKIot")]
    public class IotTemperListFunction
    {
        [Key]
        public int DevID { get; set; }
        [Display(Name ="温度")]
        public string Temperature { get; set; }
        [Display(Name = "湿度")]
        public string humidity { get; set; }//湿度 humidity
        [Display(Name = "设备IP")]
        public string IotDevIP { get; set; }
        [Display(Name = "工厂名称")]
        public string FuncName { get; set; }
        [Display(Name = "数据上传时间")]
        public DateTime Updatadate { get; set; }
    }
}
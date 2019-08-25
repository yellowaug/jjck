using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJCKManager.Models
{
    [Table("AccStatus",Schema ="JJCK")]
    public class AccStatus
    {
        [Key]
        public int AccId { get; set; }
        [StringLength(10)]
        public string AccStatusDesc { get; set; }
    }
}
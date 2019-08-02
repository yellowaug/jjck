using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJCKManager.Models
{
    [Table("Datastatus",Schema ="JJCK")]
    public class Datastatus
    {
        [Key]
        public int DaId { get; set; }
        [StringLength(50)]
        public string StatusDesc { get; set; }


    }
}
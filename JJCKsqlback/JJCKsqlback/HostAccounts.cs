namespace JJCKsqlback
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HostAccounts
    {
        [Key]
        public int HostID { get; set; }

        [Required]
        [StringLength(100)]
        public string HostName { get; set; }

        [Required]
        [StringLength(50)]
        public string HostAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string HostAccountNum { get; set; }

        [Required]
        [StringLength(50)]
        public string HostAccountPas { get; set; }

        public DateTime InputDate { get; set; }

        public string HostNote { get; set; }
    }
}

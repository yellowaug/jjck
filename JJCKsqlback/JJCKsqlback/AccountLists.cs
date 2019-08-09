namespace JJCKsqlback
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AccountLists
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNum { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountPassW { get; set; }

        public DateTime CreateTime { get; set; }

        public string Note { get; set; }

        public int? Urllist_ID { get; set; }

        public int WebListId { get; set; }

        public virtual Urllists Urllists { get; set; }

        public virtual WebLists WebLists { get; set; }
    }
}

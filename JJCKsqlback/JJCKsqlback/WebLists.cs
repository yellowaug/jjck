namespace JJCKsqlback
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WebLists
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WebLists()
        {
            AccountLists = new HashSet<AccountLists>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Website { get; set; }

        [Required]
        [StringLength(200)]
        public string WebsiteUrl { get; set; }

        public string Note { get; set; }

        public int UrllistID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountLists> AccountLists { get; set; }

        public virtual Urllists Urllists { get; set; }
    }
}

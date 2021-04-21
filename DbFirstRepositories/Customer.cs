namespace DbFirstRepositories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int Gender { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(100)]
        public string ShipAddress_City { get; set; }

        public string ShipAddress_Street { get; set; }

        public string ShipAddress_Country { get; set; }

        public string ShipAddress_PostCode { get; set; }

        [StringLength(100)]
        public string InvoiceAddress_City { get; set; }

        public string InvoiceAddress_Street { get; set; }

        public string InvoiceAddress_Country { get; set; }

        public string InvoiceAddress_PostCode { get; set; }

        public bool IsRemoved { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}

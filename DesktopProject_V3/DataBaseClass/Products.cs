namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Suppliers = new HashSet<Suppliers>();
            Types_Products = new HashSet<Types_Products>();
            Warehouses = new HashSet<Warehouses>();
        }

        [Key]
        public int ID_Product { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfProduct { get; set; }

        public byte[] AvatarOfProduct { get; set; }

        public int? ImagesOfProduct { get; set; }

        public int Price { get; set; }

        public int? Comments { get; set; }

        public string DescriptionOfProduct { get; set; }

        public string Specifications { get; set; }

        public bool? Fix { get; set; }

        public int? Discount { get; set; }

        public virtual Comments Comments1 { get; set; }

        public virtual ImagesOfProduct ImagesOfProduct1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Suppliers> Suppliers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Types_Products> Types_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouses> Warehouses { get; set; }
    }
}

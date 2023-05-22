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
            Orders_Products = new HashSet<Orders_Products>();
            Suppliers = new HashSet<Suppliers>();
            Types_Products = new HashSet<Types_Products>();
            Warehouses = new HashSet<Warehouses>();
        }
        public Products(string name, byte[] avatar, int price, string desc, int disc, string spec)
        {
            this.NameOfProduct = name;
            this.AvatarOfProduct = avatar;
            this.Price = price;
            this.DescriptionOfProduct = desc;
            this.Specifications = spec;
            this.Discount = disc;

        }
        [Key]
        public int ID_Product { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfProduct { get; set; }

        public byte[] AvatarOfProduct { get; set; }

        public int Price { get; set; }

        public string DescriptionOfProduct { get; set; }

        public string Specifications { get; set; }

        public bool? Fix { get; set; }

        public int? Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders_Products> Orders_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Suppliers> Suppliers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Types_Products> Types_Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouses> Warehouses { get; set; }
    }
}

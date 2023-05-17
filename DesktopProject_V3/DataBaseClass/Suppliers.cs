namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Suppliers
    {
        public Suppliers()
        {

        }
        public Suppliers(string name, int id)
        {
            this.NameOfSupplier = name;
            this.ID_Product = id;
        }
        [Key]
        public int ID_Supplier { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfSupplier { get; set; }

        public int? ID_Product { get; set; }

        public virtual Products Products { get; set; }
    }
}

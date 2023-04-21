namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Suppliers
    {
        [Key]
        public int ID_Supplier { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfSupplier { get; set; }

        public int? ID_Product { get; set; }

        public virtual Products Products { get; set; }
    }
}

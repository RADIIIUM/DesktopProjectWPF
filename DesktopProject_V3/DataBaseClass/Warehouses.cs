namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Warehouses
    {
        public Warehouses() { }
        public Warehouses(int id, int cout)
        {
            this.ID_Product = id;
            this.CountOfProduct = cout;
        }
        [Key]
        public int ID_Cell { get; set; }

        public int? CountOfProduct { get; set; }

        public int? ID_Product { get; set; }

        public virtual Products Products { get; set; }
    }
}

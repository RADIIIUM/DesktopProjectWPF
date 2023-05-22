namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders_Products
    {
        [Key]
        public int ID_OP { get; set; }

        public int? ID_Order { get; set; }

        public int? ID_Product { get; set; }

        public int? CountOfProduct { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }
    }
}

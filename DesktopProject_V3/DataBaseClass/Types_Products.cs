namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Types_Products
    {
        public Types_Products() 
        { 

        }
        public Types_Products(int idt, int idp)
        {
            this.ID_Product = idp;
            this.ID_Type = idt;
        }
        [Key]
        public int ID_TP { get; set; }

        public int? ID_Type { get; set; }

        public int? ID_Product { get; set; }

        public virtual Products Products { get; set; }

        public virtual TypesOfProducts TypesOfProducts { get; set; }
    }
}

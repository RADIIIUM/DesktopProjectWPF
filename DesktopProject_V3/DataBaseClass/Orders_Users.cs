namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders_Users
    {
        [Key]
        public int ID_OU { get; set; }

        public int? ID_Order { get; set; }

        [StringLength(45)]
        public string LoginOfUser { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Users Users { get; set; }
    }
}

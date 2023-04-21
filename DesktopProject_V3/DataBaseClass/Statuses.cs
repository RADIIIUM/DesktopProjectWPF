namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Statuses
    {
        [Key]
        public int ID_Status { get; set; }

        [Required]
        [StringLength(45)]
        public string NameOfStatus { get; set; }
    }
}

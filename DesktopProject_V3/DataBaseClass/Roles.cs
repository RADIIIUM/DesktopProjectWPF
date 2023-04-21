namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Roles
    {
        public Roles(string name, string login)
        {
            this.LoginOfUsers = login;
            this.NameOfRole = name;
        }
        public Roles()
        {

        }
        [Key]
        public int ID_Role { get; set; }

        [Required]
        [StringLength(45)]
        public string NameOfRole { get; set; }

        [StringLength(45)]
        public string LoginOfUsers { get; set; }

        public virtual Users Users { get; set; }
    }
}

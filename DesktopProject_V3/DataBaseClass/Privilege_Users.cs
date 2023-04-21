namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Privilege_Users
    {
        public Privilege_Users(int id_privilig, string login)
        {
            this.ID_PrivilegeUsers = id_privilig;
            this.LoginUser = login;
        }
        public Privilege_Users()
        {

        }
        [Key]
        public int ID_PrivilegeUsers { get; set; }

        public int? ID_Provolege { get; set; }

        [StringLength(45)]
        public string LoginUser { get; set; }

        public virtual Privilege Privilege { get; set; }

        public virtual Users Users { get; set; }
    }
}

namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Privilege_Users = new HashSet<Privilege_Users>();
            Roles = new HashSet<Roles>();
        }

        public Users(string login, string pass, string email, byte[] avatar)
        {
            this.LoginOfUser = login;
            this.PasswordOfUser = pass;
            this.NameOfUser = login;
            this.Email = email;
            this.Avatar = avatar;
        }

        [Key]
        [StringLength(45)]
        public string LoginOfUser { get; set; }

        [Required]
        [StringLength(45)]
        public string PasswordOfUser { get; set; }

        [Required]
        [StringLength(45)]
        public string NameOfUser { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(45)]
        public string Telephone { get; set; }

        [StringLength(45)]
        public string Passport { get; set; }

        [StringLength(100)]
        public string AddressOfUser { get; set; }

        [StringLength(45)]
        public string INN { get; set; }

        [StringLength(45)]
        public string BankCard { get; set; }

        public int? Balance { get; set; }

        public string DescriptionOfUser { get; set; }

        public byte[] Avatar { get; set; }

        public int? ID_HistoryOfUser { get; set; }

        public virtual HistoryOfUser HistoryOfUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Privilege_Users> Privilege_Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Roles> Roles { get; set; }
    }
}

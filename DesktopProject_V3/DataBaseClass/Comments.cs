namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comments()
        {
            Products = new HashSet<Products>();
        }

        public Comments(string Title, string Desc, string Login, int Rating, Products prd)
        {
            this.ParagraphOfComment = Title;
            this.DescriptionOfComment = Desc;
            this.LoginOfUser = Login;
            this.Rating = Rating;
            this.Products.Add(prd);
        }

        [Key]
        public int ID_Comment { get; set; }

        public string ParagraphOfComment { get; set; }

        public string DescriptionOfComment { get; set; }

        [Required]
        [StringLength(45)]
        public string LoginOfUser { get; set; }

        public int? Rating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}

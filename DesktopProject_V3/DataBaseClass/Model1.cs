using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DesktopProject_V3.DataBaseClass
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<HistoryOfUser> HistoryOfUser { get; set; }
        public virtual DbSet<ImagesOfProduct> ImagesOfProduct { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Privilege> Privilege { get; set; }
        public virtual DbSet<Privilege_Users> Privilege_Users { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Types_Products> Types_Products { get; set; }
        public virtual DbSet<TypesOfProducts> TypesOfProducts { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Warehouses> Warehouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>()
                .Property(e => e.ParagraphOfComment)
                .IsUnicode(false);

            modelBuilder.Entity<Comments>()
                .Property(e => e.DescriptionOfComment)
                .IsUnicode(false);

            modelBuilder.Entity<Comments>()
                .Property(e => e.LoginOfUser)
                .IsUnicode(false);

            modelBuilder.Entity<Comments>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Comments1)
                .HasForeignKey(e => e.Comments)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HistoryOfUser>()
                .Property(e => e.ActionOfUser)
                .IsUnicode(false);

            modelBuilder.Entity<HistoryOfUser>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.HistoryOfUser)
                .HasForeignKey(e => e.ID_HistoryOfUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ImagesOfProduct>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.ImagesOfProduct1)
                .HasForeignKey(e => e.ImagesOfProduct)
                .WillCascadeOnDelete();

            modelBuilder.Entity<News>()
                .Property(e => e.Paragraph)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.DescriptionOfNews)
                .IsUnicode(false);

            modelBuilder.Entity<Privilege>()
                .Property(e => e.NameOfStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Privilege>()
                .HasMany(e => e.Privilege_Users)
                .WithOptional(e => e.Privilege)
                .HasForeignKey(e => e.ID_Provolege)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Privilege_Users>()
                .Property(e => e.LoginUser)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.NameOfProduct)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.DescriptionOfProduct)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Specifications)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Suppliers)
                .WithOptional(e => e.Products)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Types_Products)
                .WithOptional(e => e.Products)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Warehouses)
                .WithOptional(e => e.Products)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Roles>()
                .Property(e => e.NameOfRole)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.LoginOfUsers)
                .IsUnicode(false);

            modelBuilder.Entity<Statuses>()
                .Property(e => e.NameOfStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Suppliers>()
                .Property(e => e.NameOfSupplier)
                .IsUnicode(false);

            modelBuilder.Entity<TypesOfProducts>()
                .Property(e => e.NameOfType)
                .IsUnicode(false);

            modelBuilder.Entity<TypesOfProducts>()
                .HasMany(e => e.Types_Products)
                .WithOptional(e => e.TypesOfProducts)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Users>()
                .Property(e => e.LoginOfUser)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.PasswordOfUser)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.NameOfUser)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Passport)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.AddressOfUser)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.INN)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.BankCard)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.DescriptionOfUser)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Privilege_Users)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.LoginUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Roles)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.LoginOfUsers)
                .WillCascadeOnDelete();
        }
    }
}

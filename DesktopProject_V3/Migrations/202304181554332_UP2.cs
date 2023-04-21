namespace DesktopProject_V3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UP2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ID_HistoryOfUser", "dbo.HistoryOfUser");
            DropForeignKey("dbo.Roles", "LoginOfUsers", "dbo.Users");
            DropForeignKey("dbo.Privilege_Users", "LoginUser", "dbo.Users");
            DropForeignKey("dbo.Privilege_Users", "ID_Provolege", "dbo.Privilege");
            DropForeignKey("dbo.Products", "Comments", "dbo.Comments");
            DropForeignKey("dbo.Warehouses", "ID_Product", "dbo.Products");
            DropForeignKey("dbo.Suppliers", "ID_Product", "dbo.Products");
            DropForeignKey("dbo.Products", "ImagesOfProduct", "dbo.ImagesOfProduct");
            DropIndex("dbo.Roles", new[] { "LoginOfUsers" });
            DropIndex("dbo.Privilege_Users", new[] { "LoginUser" });
            DropIndex("dbo.Privilege_Users", new[] { "ID_Provolege" });
            DropIndex("dbo.Users", new[] { "ID_HistoryOfUser" });
            DropIndex("dbo.Warehouses", new[] { "ID_Product" });
            DropIndex("dbo.Suppliers", new[] { "ID_Product" });
            DropIndex("dbo.Products", new[] { "Comments" });
            DropIndex("dbo.Products", new[] { "ImagesOfProduct" });
            DropTable("dbo.Statuses");
            DropTable("dbo.Roles");
            DropTable("dbo.Privilege");
            DropTable("dbo.Privilege_Users");
            DropTable("dbo.Users");
            DropTable("dbo.HistoryOfUser");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Suppliers");
            DropTable("dbo.ImagesOfProduct");
            DropTable("dbo.Products");
            DropTable("dbo.Comments");

            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID_Comment = c.Int(nullable: false, identity: true),
                        ParagraphOfComment = c.String(unicode: false),
                        DescriptionOfComment = c.String(unicode: false),
                        LoginOfUser = c.String(nullable: false, maxLength: 45, unicode: false),
                        Rating = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Comment);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID_Product = c.Int(nullable: false, identity: true),
                        NameOfProduct = c.String(nullable: false, maxLength: 100, unicode: false),
                        AvatarOfProduct = c.Binary(),
                        ImagesOfProduct = c.Int(),
                        Price = c.Int(nullable: false),
                        Comments = c.Int(),
                        DescriptionOfProduct = c.String(unicode: false),
                        Specifications = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID_Product)
                .ForeignKey("dbo.ImagesOfProduct", t => t.ImagesOfProduct, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.Comments, cascadeDelete: true)
                .Index(t => t.ImagesOfProduct)
                .Index(t => t.Comments);
            
            CreateTable(
                "dbo.ImagesOfProduct",
                c => new
                    {
                        ID_Images = c.Int(nullable: false, identity: true),
                        ImageOfProduct = c.Binary(),
                    })
                .PrimaryKey(t => t.ID_Images);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID_Supplier = c.Int(nullable: false, identity: true),
                        NameOfSupplier = c.String(nullable: false, maxLength: 100, unicode: false),
                        ID_Product = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Supplier)
                .ForeignKey("dbo.Products", t => t.ID_Product, cascadeDelete: true)
                .Index(t => t.ID_Product);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        ID_Cell = c.Int(nullable: false, identity: true),
                        CountOfProduct = c.Int(),
                        ID_Product = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Cell)
                .ForeignKey("dbo.Products", t => t.ID_Product, cascadeDelete: true)
                .Index(t => t.ID_Product);
            
            CreateTable(
                "dbo.HistoryOfUser",
                c => new
                    {
                        ID_History = c.Int(nullable: false, identity: true),
                        DateOfAction = c.DateTime(),
                        ReadAction = c.Boolean(),
                        ActionOfUser = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID_History);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        LoginOfUser = c.String(nullable: false, maxLength: 45, unicode: false),
                        PasswordOfUser = c.String(nullable: false, maxLength: 45, unicode: false),
                        NameOfUser = c.String(nullable: false, maxLength: 45, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Telephone = c.String(maxLength: 45, unicode: false),
                        Passport = c.String(maxLength: 45, unicode: false),
                        AddressOfUser = c.String(maxLength: 100, unicode: false),
                        INN = c.String(maxLength: 45, unicode: false),
                        BankCard = c.String(maxLength: 45, unicode: false),
                        Balance = c.Int(),
                        DescriptionOfUser = c.String(unicode: false),
                        Avatar = c.Binary(),
                        ID_HistoryOfUser = c.Int(),
                    })
                .PrimaryKey(t => t.LoginOfUser)
                .ForeignKey("dbo.HistoryOfUser", t => t.ID_HistoryOfUser, cascadeDelete: true)
                .Index(t => t.ID_HistoryOfUser);
            
            CreateTable(
                "dbo.Privilege_Users",
                c => new
                    {
                        ID_PrivilegeUsers = c.Int(nullable: false, identity: true),
                        ID_Provolege = c.Int(),
                        LoginUser = c.String(maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.ID_PrivilegeUsers)
                .ForeignKey("dbo.Privilege", t => t.ID_Provolege, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LoginUser, cascadeDelete: true)
                .Index(t => t.ID_Provolege)
                .Index(t => t.LoginUser);
            
            CreateTable(
                "dbo.Privilege",
                c => new
                    {
                        ID_Status = c.Int(nullable: false, identity: true),
                        NameOfStatus = c.String(nullable: false, maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.ID_Status);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID_Role = c.Int(nullable: false, identity: true),
                        NameOfRole = c.String(nullable: false, maxLength: 45, unicode: false),
                        LoginOfUsers = c.String(maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.ID_Role)
                .ForeignKey("dbo.Users", t => t.LoginOfUsers, cascadeDelete: true)
                .Index(t => t.LoginOfUsers);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        ID_Status = c.Int(nullable: false, identity: true),
                        NameOfStatus = c.String(nullable: false, maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.ID_Status);
            
        }
        
    }
}

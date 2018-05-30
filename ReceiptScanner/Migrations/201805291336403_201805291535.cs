namespace ReceiptScanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201805291535 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Currency_Code = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currency", t => t.Currency_Code)
                .Index(t => t.Currency_Code);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Symbol = c.String(),
                        Exchange_Rate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Currency_Code = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.Currency", t => t.Currency_Code)
                .Index(t => t.Currency_Code);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                        Street_Number = c.Int(nullable: false),
                        City = c.String(),
                        ZIP_Code = c.String(),
                        State = c.String(),
                        Country_Code = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.Country_Code)
                .Index(t => t.Country_Code);
            
            CreateTable(
                "dbo.Receipt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                        Notes = c.String(),
                        Base64_Image = c.String(nullable: false),
                        Currency_Id = c.String(nullable: false, maxLength: 128),
                        Account_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Category_Id = c.Int(),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Account_Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .ForeignKey("dbo.Currency", t => t.Currency_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Store", t => t.Store_Id)
                .Index(t => t.Currency_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Crop",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Price = c.Int(nullable: false),
                        X_Coordinate = c.Int(nullable: false),
                        Y_Coordinate = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                        Receipt_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .ForeignKey("dbo.Receipt", t => t.Receipt_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Receipt_Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Language_Code = c.String(maxLength: 128),
                        Country_Code = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.Language_Code)
                .ForeignKey("dbo.Country", t => t.Country_Code)
                .Index(t => t.Language_Code)
                .Index(t => t.Country_Code);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.UserAccount",
                c => new
                    {
                        Account_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Is_Admin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_Id, t.User_Id })
                .ForeignKey("dbo.Account", t => t.Account_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "Currency_Code", "dbo.Currency");
            DropForeignKey("dbo.User", "Country_Code", "dbo.Country");
            DropForeignKey("dbo.Receipt", "Store_Id", "dbo.Store");
            DropForeignKey("dbo.Receipt", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserAccount", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserAccount", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.User", "Language_Code", "dbo.Language");
            DropForeignKey("dbo.Receipt", "Currency_Id", "dbo.Currency");
            DropForeignKey("dbo.Crop", "Receipt_Id", "dbo.Receipt");
            DropForeignKey("dbo.Crop", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Receipt", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Receipt", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.Store", "Country_Code", "dbo.Country");
            DropForeignKey("dbo.Country", "Currency_Code", "dbo.Currency");
            DropIndex("dbo.UserAccount", new[] { "User_Id" });
            DropIndex("dbo.UserAccount", new[] { "Account_Id" });
            DropIndex("dbo.User", new[] { "Country_Code" });
            DropIndex("dbo.User", new[] { "Language_Code" });
            DropIndex("dbo.Crop", new[] { "Receipt_Id" });
            DropIndex("dbo.Crop", new[] { "Category_Id" });
            DropIndex("dbo.Receipt", new[] { "Store_Id" });
            DropIndex("dbo.Receipt", new[] { "Category_Id" });
            DropIndex("dbo.Receipt", new[] { "User_Id" });
            DropIndex("dbo.Receipt", new[] { "Account_Id" });
            DropIndex("dbo.Receipt", new[] { "Currency_Id" });
            DropIndex("dbo.Store", new[] { "Country_Code" });
            DropIndex("dbo.Country", new[] { "Currency_Code" });
            DropIndex("dbo.Account", new[] { "Currency_Code" });
            DropTable("dbo.UserAccount");
            DropTable("dbo.Language");
            DropTable("dbo.User");
            DropTable("dbo.Category");
            DropTable("dbo.Crop");
            DropTable("dbo.Receipt");
            DropTable("dbo.Store");
            DropTable("dbo.Country");
            DropTable("dbo.Currency");
            DropTable("dbo.Account");
        }
    }
}

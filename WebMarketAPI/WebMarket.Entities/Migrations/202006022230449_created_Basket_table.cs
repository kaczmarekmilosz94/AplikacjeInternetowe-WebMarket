namespace WebMarket.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created_Basket_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OwnerId)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.BasketProducts",
                c => new
                    {
                        Basket_OwnerId = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Basket_OwnerId, t.Product_Id })
                .ForeignKey("dbo.Baskets", t => t.Basket_OwnerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Basket_OwnerId)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.BasketProducts", "Basket_OwnerId", "dbo.Baskets");
            DropForeignKey("dbo.Baskets", "OwnerId", "dbo.Users");
            DropIndex("dbo.BasketProducts", new[] { "Product_Id" });
            DropIndex("dbo.BasketProducts", new[] { "Basket_OwnerId" });
            DropIndex("dbo.Baskets", new[] { "OwnerId" });
            DropTable("dbo.BasketProducts");
            DropTable("dbo.Baskets");
        }
    }
}

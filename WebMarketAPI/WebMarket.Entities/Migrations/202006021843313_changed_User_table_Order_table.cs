namespace WebMarket.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_User_table_Order_table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Id", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "Id" });
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String());
            CreateIndex("dbo.Products", "OrderId");
            AddForeignKey("dbo.Products", "OrderId", "dbo.Orders", "Id");
            DropColumn("dbo.Orders", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "OrderId", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "OrderId" });
            AlterColumn("dbo.Users", "PhoneNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "IsAdmin");
            CreateIndex("dbo.Orders", "Id");
            AddForeignKey("dbo.Orders", "Id", "dbo.Products", "Id");
        }
    }
}

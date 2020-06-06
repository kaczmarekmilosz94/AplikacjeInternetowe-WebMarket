namespace WebMarket.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_Order_Config : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PurchaseTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PurchaseTime", c => c.DateTime(nullable: false));
        }
    }
}

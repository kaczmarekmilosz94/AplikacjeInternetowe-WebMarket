namespace WebMarket.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_UserConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Adress", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Users", "Adress", c => c.String());
        }
    }
}

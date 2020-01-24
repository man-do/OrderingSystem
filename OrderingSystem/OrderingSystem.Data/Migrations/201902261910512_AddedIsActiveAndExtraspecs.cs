namespace OrderingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsActiveAndExtraspecs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ExtraSpecifications", c => c.String());
            AddColumn("dbo.Orders", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Message", c => c.String());
            DropColumn("dbo.Orders", "IsActive");
            DropColumn("dbo.Orders", "ExtraSpecifications");
        }
    }
}

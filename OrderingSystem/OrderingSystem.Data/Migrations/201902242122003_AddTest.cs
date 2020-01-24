namespace OrderingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Message");
        }
    }
}

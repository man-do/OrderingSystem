namespace OrderingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaxidriverIdToOrdersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TaxiDriverId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TaxiDriverId");
        }
    }
}

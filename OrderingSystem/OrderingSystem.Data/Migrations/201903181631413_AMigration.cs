namespace OrderingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AMigration : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AuditLogs");
            //AddColumn("dbo.Orders", "ExtraSpecifications", c => c.String());
            //AddColumn("dbo.Orders", "IsActive", c => c.Boolean(nullable: false));
            //AddColumn("dbo.Orders", "TaxiDriverId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AuditLogs", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AuditLogs", "Thread", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.AuditLogs", "Level", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.AuditLogs", "Logger", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.AuditLogs", "Message", c => c.String(maxLength: 4000, unicode: false));
            AlterColumn("dbo.AuditLogs", "Exception", c => c.String(maxLength: 2000, unicode: false));
            AddPrimaryKey("dbo.AuditLogs", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AuditLogs");
            AlterColumn("dbo.AuditLogs", "Exception", c => c.String());
            AlterColumn("dbo.AuditLogs", "Message", c => c.String());
            AlterColumn("dbo.AuditLogs", "Logger", c => c.String());
            AlterColumn("dbo.AuditLogs", "Level", c => c.String());
            AlterColumn("dbo.AuditLogs", "Thread", c => c.String());
            AlterColumn("dbo.AuditLogs", "Id", c => c.String(nullable: false, maxLength: 128));
            //DropColumn("dbo.Orders", "TaxiDriverId");
            //DropColumn("dbo.Orders", "IsActive");
            //DropColumn("dbo.Orders", "ExtraSpecifications");
            AddPrimaryKey("dbo.AuditLogs", "Id");
        }
    }
}

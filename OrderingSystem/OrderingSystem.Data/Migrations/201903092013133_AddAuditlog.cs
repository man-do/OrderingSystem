namespace OrderingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Thread = c.String(),
                    Level = c.String(),
                    Logger = c.String(),
                    Message = c.String(),
                    Exception = c.String(),
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}

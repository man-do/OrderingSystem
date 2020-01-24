namespace OrderingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignOtherTestUsersToTheirRoles : DbMigration
    {
        public override void Up()
        {
			Sql("insert into AspNetUserRoles (UserId, RoleId) values ('bd30035c-1de1-4bd8-9999-df5d68579c04', 4)");
			Sql("insert into AspNetUserRoles (UserId, RoleId) values ('46dd3cf8-ed55-4819-bab0-60b8169e5b80', 5)");
		}
        
        public override void Down()
        {
        }
    }
}

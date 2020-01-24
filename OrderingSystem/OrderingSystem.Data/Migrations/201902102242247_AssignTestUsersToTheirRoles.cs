namespace OrderingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignTestUsersToTheirRoles : DbMigration
    {
        public override void Up()
        {
			Sql("insert into AspNetUserRoles (UserId, RoleId) values ('a9ebf2fb-656c-4d3c-a7bb-ab9a7c715923', 1)");
			Sql("insert into AspNetUserRoles (UserId, RoleId) values ('c4e58068-a7f3-458c-aeb0-98cc1bd1a53b', 3)");
			Sql("insert into AspNetUserRoles (UserId, RoleId) values ('d7e88156-ced7-47a6-9cf4-65a8f8dbd5c0', 2)");
        }
        
        public override void Down()
        {
        }
    }
}

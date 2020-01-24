namespace OrderingSystem.Data.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddRoles : DbMigration
	{
		public override void Up()
		{
			Sql("insert into AspNetRoles (Id, Name) values (1, 'Administrator')");
			Sql("insert into AspNetRoles (Id, Name) values (2, 'Client')");
			Sql("insert into AspNetRoles (Id, Name) values (3, 'Chef')");
			Sql("insert into AspNetRoles (Id, Name) values (4, 'KitchenStaff')");
			Sql("insert into AspNetRoles (Id, Name) values (5, 'Taxi')");
		}
		
		public override void Down()
		{
		}
	}
}

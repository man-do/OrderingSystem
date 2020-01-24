namespace OrderingSystem.Data.Data.Entities
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class OSEntities : DbContext
	{
		public OSEntities()
			: base("DefaultConnection")
		{
            Database.SetInitializer<OSEntities>(new CreateDatabaseIfNotExists<OSEntities>());

        }


        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
		public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
		public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
		public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
		public virtual DbSet<Dish> Dishes { get; set; }
		public virtual DbSet<Igredient> Igredients { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Staff_Capacity> Staff_Capacity { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Address>()
				.Property(e => e.Address1)
				.IsUnicode(false);

			modelBuilder.Entity<Address>()
				.HasMany(e => e.AspNetUsers)
				.WithMany(e => e.Addresses)
				.Map(m => m.ToTable("User_Addresses").MapLeftKey("AddressId").MapRightKey("UserId"));

			modelBuilder.Entity<AspNetRole>()
				.HasMany(e => e.AspNetUsers)
				.WithMany(e => e.AspNetRoles)
				.Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

			modelBuilder.Entity<AspNetUser>()
				.HasMany(e => e.AspNetUserClaims)
				.WithRequired(e => e.AspNetUser)
				.HasForeignKey(e => e.UserId);

			modelBuilder.Entity<AspNetUser>()
				.HasMany(e => e.AspNetUserLogins)
				.WithRequired(e => e.AspNetUser)
				.HasForeignKey(e => e.UserId);

			modelBuilder.Entity<AspNetUser>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.AspNetUser)
				.HasForeignKey(e => e.ClientId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AspNetUser>()
				.HasMany(e => e.Orders1)
				.WithOptional(e => e.AspNetUser1)
				.HasForeignKey(e => e.KitchenStaffId);

			modelBuilder.Entity<AspNetUser>()
				.HasOptional(e => e.Staff_Capacity)
				.WithRequired(e => e.AspNetUser)
				.WillCascadeOnDelete();

			modelBuilder.Entity<Dish>()
				.Property(e => e.Price)
				.HasPrecision(18, 3);

			modelBuilder.Entity<Dish>()
				.Property(e => e.Description)
				.IsUnicode(false);

			modelBuilder.Entity<Dish>()
				.Property(e => e.Rating)
				.HasPrecision(4, 2);

			modelBuilder.Entity<Dish>()
				.HasMany(e => e.Igredients)
				.WithMany(e => e.Dishes)
				.Map(m => m.ToTable("Dish_Ingredient").MapLeftKey("DishId").MapRightKey("IngredientId"));

			modelBuilder.Entity<Dish>()
				.HasMany(e => e.Orders)
				.WithMany(e => e.Dishes)
				.Map(m => m.ToTable("Order_Dish").MapLeftKey("DishId").MapRightKey("OrderId"));

			modelBuilder.Entity<Igredient>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Order>()
				.Property(e => e.OrderPrice)
				.HasPrecision(18, 3);
		}
	}
}

namespace OrderingSystem.Data.Data.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class Order
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Order()
		{
			Dishes = new HashSet<Dish>();
		}

		public int OrderId { get; set; }

		public DateTime OrderTime { get; set; }

		[Required]
		[StringLength(128)]
		public string ClientId { get; set; }

		public int DeliveryAddressId { get; set; }

		public bool OrderPrepared { get; set; }

		public bool OrderDeliverd { get; set; }

		public decimal OrderPrice { get; set; }

		public TimeSpan DeliveryTime { get; set; }

		[StringLength(128)]
		public string KitchenStaffId { get; set; }

		//Added
		public string ExtraSpecifications { get; set; }

		public bool IsActive { get; set; }

		[StringLength(128)]
		public string TaxiDriverId { get; set; }



		public virtual AspNetUser AspNetUser { get; set; }

		public virtual AspNetUser AspNetUser1 { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Dish> Dishes { get; set; }
	}
}

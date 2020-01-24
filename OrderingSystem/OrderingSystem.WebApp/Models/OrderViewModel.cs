using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrderingSystem.Data.Common;

namespace OrderingSystem.WebApp.Models
{
	public class OrderViewModel
	{
		public int OrderId { get; set; }

		public DateTime OrderTime { get; set; }

		[Required]
		[StringLength(128)]
		public string ClientId { get; set; }

		public string DeliveryAddress { get; set; }

		public bool OrderPrepared { get; set; }

		public bool OrderDeliverd { get; set; }

		public decimal OrderPrice { get; set; }

		public TimeSpan DeliveryTime { get; set; }

		[StringLength(128)]
		public string KitchenStaffId { get; set; }

		public virtual AspNetUser AspNetUser { get; set; }

		public virtual ICollection<Dish> Dishes { get; set; }

		// Added
		public string clientUserName { get; set; }
		public string ExtraSpecs { get; set; }
		[StringLength(128)]
		public string TaxiDriverId { get; set; }
	}
}
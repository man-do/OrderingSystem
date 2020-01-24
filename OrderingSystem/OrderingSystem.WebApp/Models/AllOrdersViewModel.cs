using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderingSystem.Data.Common;
using OrderingSystem.WebApp.Models;

namespace OrderingSystem.WebApp.Models
{
	public class AllOrdersViewModel
	{
		public List<OrderViewModel> OrdersModels { get; set; }
		public PagingInfo PagingInfo { get; set; }
	}
}
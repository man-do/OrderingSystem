using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.WebApp.Models
{
	public class IgredientCheckboxViewModel
	{
		public int IgredientId { get; set; }
		public string Name { get; set; }
		public bool IsChecked { get; set; }
	}
}
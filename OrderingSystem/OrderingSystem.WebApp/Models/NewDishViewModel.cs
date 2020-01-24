using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.WebApp.Models
{
	public class NewDishViewModel
	{
		public int DishId { get; set; }

		[Required]
		[StringLength(250)]
		public string Name { get; set; }

		[Range(0, 10000, ErrorMessage = "Price value must be between 0 and 10000 $")]
		[Required]
		public decimal Price { get; set; }

		[Required]
		public string Description { get; set; }
		
		public byte[] Image { get; set; }

		public bool IsVegan { get; set; }

		public bool IsPescatarian { get; set; }

		public bool IsVegetarian { get; set; }

		public bool HasPeanuts { get; set; }

		public bool HasSeafood { get; set; }
		
		public virtual List<IgredientCheckboxViewModel> Igredients { get; set; }
	}
}

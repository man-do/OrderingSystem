using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderingSystem.Data.Common;

namespace OrderingSystem.WebApp.Models
{
    public class DishListViewModel
    {
        public IEnumerable<Dish> Dishes { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentSearchPhrase { get; set; }

        public string CurrentDishType { get; set; }

        public string CurrentAllergy { get; set; }

        public decimal CurrentRating { get; set; }

        public Igredient CurrentIgredient { get; set; }

    }

    public class ListDishesViewModel
    {
        public IEnumerable<Dish> Dishes { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }

}
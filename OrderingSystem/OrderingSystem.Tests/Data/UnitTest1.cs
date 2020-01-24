using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using OrderingSystem.Data.Data.Concrete;
using OrderingSystem.Data.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using OrderingSystem.Data.Data.Abstract;

namespace OrderingSystem.Tests.Data
{
    [TestClass]
    public class Generic_Repository
    {
        Mock<IRepository<Dish>> mock = new Mock<IRepository<Dish>>();

        [TestMethod]
        public void GetAll_Works()
        {
            /*
            var dish1 = new Dish()
            {
                DishId = 5,
                Name = "Simple Dish",
                Price = 246,
                Igredients = null,
                Image = null,
                Description = "A normal Dish",
                Rating = 2,
                Enabled = true,
                HasPeanuts = true,
                IsPescatarian = true,
                HasSeafood = true,
                IsVegan = true,
                IsVegetarian = true,
                Orders = null,
            };

            var dish2 = new Dish()
            {
                DishId = 4,
                Name = "Simple Dish",
                Price = 246,
                Igredients = null,
                Image = null,
                Description = "A normal Dish",
                Rating = 2,
                Enabled = true,
                HasPeanuts = true,
                IsPescatarian = true,
                HasSeafood = true,
                IsVegan = true,
                IsVegetarian = true,
                Orders = null,
            };

            var returnList = new[] { dish1, dish2 };

            mock.Setup(p => p.GetAll()).Returns(returnList);
            */
        }
    }
}

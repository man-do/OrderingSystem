using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.WebApp.Models;
using OrderingSystem.Data.Data.Abstract;
using System.Collections.Generic;
using OrderingSystem.WebApp.Controllers;

namespace OrderingSystem.Tests.Business
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void AddDish_Works()
        {
            // Arrange - create some test products
            Dish dish1 = new Dish { DishId = 1, Name = "dish 1" };
            Dish dish2 = new Dish { DishId = 2, Name = "dish 2" };
            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddDish(dish1, 1);
            target.AddDish(dish2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Dish, dish1);
            Assert.AreEqual(results[1].Dish, dish2);
        }

        [TestMethod]
        public void RemoveLine_Works()
        {
            // Arrange - create new dishes
            Dish dish1 = new Dish { DishId = 1, Name = "dish 1" };
            Dish dish2 = new Dish { DishId = 2, Name = "dish 2" };

            // Arrange - create new cart
            Cart cart = new Cart();

            // Act - remove line
            cart.AddDish(dish1);
            cart.AddDish(dish2);
            cart.RemoveLine(dish1);
            CartLine[] results = cart.Lines.ToArray();


            // Assert
            Assert.AreEqual(results.Length, 1);
            Assert.AreEqual(results[0].Dish, dish2);
        }

        [TestMethod]
        public void ClearCart_Works()
        {
            // Arrange - create new dishes
            Dish dish1 = new Dish { DishId = 1, Name = "dish 1" };
            Dish dish2 = new Dish { DishId = 2, Name = "dish 2" };

            // Arrange - create new cart
            Cart cart = new Cart();

            // Act - remove line
            cart.AddDish(dish1);
            cart.AddDish(dish2);
            cart.ClearCart();
            CartLine[] results = cart.Lines.ToArray();

            // Assert 
            Assert.AreEqual(results.Length, 0);
        }
    }

    [TestClass]
    public class CartControllerTest
    {
        /*
        [TestMethod]
        public void AddToCart_Works()
        {
            // Arrange - create new dishes
            Dish dish1 = new Dish { DishId = 1, Name = "dish 1" };
            Dish dish2 = new Dish { DishId = 2, Name = "dish 2" };

            // Arrange - create fake repo
            Mock<IRepository<Dish>> mock = new Mock<IRepository<Dish>>();
            mock.Setup(r => r.GetAll()).Returns(new List<Dish>()
            {
                dish1 , dish2
            });
            CartController cartController = new CartController(mock.Object);

            // Act - add to cart
            cartController.AddToCart(dish1.DishId, "nthn");
            cartController.AddToCart(dish1.DishId, "nthn");
            CartLine[] results = cartController.GetCart().Lines.ToArray();

            // Assert - check the cart
            Assert.AreEqual(results[0].Dish , dish1);
            Assert.AreEqual(results[0].Quantity, 2);
        }
        */
    }
}

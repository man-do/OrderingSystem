using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.WebApp.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// Adds dishes to the cart instance .
        /// </summary>
        /// <param name="dishParam">The dish instance.</param>
        /// <param name="quantityParam">The quantity, default is 1.</param>
        public void AddDish(Dish dishParam , decimal quantityParam = 1)
        {
            CartLine line = lineCollection
                .Where(l => l.Dish.DishId == dishParam.DishId)
                .ToList()
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine(dishParam, quantityParam));
            }
            else
                line.Quantity += quantityParam;
        }

        /// <summary>
        /// Removes the line from cart.
        /// </summary>
        /// <param name="dishParam">The dish parameter.</param>
        public void RemoveLine(Dish dishParam) => lineCollection.RemoveAll(l => l.Dish.DishId == dishParam.DishId);

        /// <summary>
        /// Clears the cart.
        /// </summary>
        public void ClearCart() => lineCollection.Clear();

        /// <summary>
        /// Gets the line.
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        public IEnumerable<CartLine> Lines
        {
            get => lineCollection;
        }

        /// <summary>
        /// Returns the total price of the cart.
        /// </summary>
        /// <returns></returns>
        public decimal TotalPrice()
        {
            decimal totalPrice = 0;

            foreach(var item in lineCollection)
            {
                totalPrice += item.Quantity * item.Dish.Price;
            }

            return totalPrice;
        }

        /// <summary>
        /// Returns the total number of products.
        /// </summary>
        /// <returns>Decimal value of total products.</returns>
        public decimal TotalProducts()
        {
            decimal count = 0;
            foreach (var line in lineCollection)
            {
                count += line.Quantity;
            }
            return count;
        }

        /// <summary>
        /// Gets or sets the address of the delivery.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public Address Address { get; set; }
    }

    public class CartLine
    {
        public CartLine(Dish dishParam , decimal quantityParam)
        {
            this.Dish = dishParam;
            this.Quantity = quantityParam;
        }

        public Dish Dish { get; set; }

        public decimal Quantity { get; set; }
    }

    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }

}
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Concrete;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingSystem.WebApp.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private OSEntities context = new OSEntities();
        //Same as using a unit of work
        private IRepository<Address> addressRepo;
        private IRepository<AspNetUser> userRepo;
        private IRepository<Order> orderRepo;
        private IRepository<Dish> dishRepo;

        public CheckoutController()
        {
            this.addressRepo = new DbRepository<Address>(context);
            this.userRepo = new DbRepository<AspNetUser>(context);
            this.orderRepo = new DbRepository<Order>(context);
            this.dishRepo = new DbRepository<Dish>(context);
        }

        // GET: Checkout/CompleteCart
        [HttpGet]
        public ActionResult CompleteCart()
        {
            string name = this.HttpContext.User.Identity.Name;

            var currentUser = this.userRepo.GetAll().Where(u => u.UserName == name).First();

            var model = new CheckoutViewModel()
            {
                AllUserAddresses = currentUser.Addresses.ToList(),
            };
            return View(model);
        }

        //POST: /Checkout/CompleteCart
        [HttpPost]
        public RedirectToRouteResult CompleteCart(CheckoutViewModel model)
        {
            DateTime orderTime = DateTime.Now;

            Order order = null;

            int addressId = -1;

            Order verifyInsertion = null;

            string name = this.HttpContext.User.Identity.Name;

            var currentUser = this.userRepo.GetAll().Where(u => u.UserName == name).First();

            //Check if user entered new address or used old one
            if (model.Address == null || model.Address == string.Empty)
            {
                if (model.NewAddress != null && model.NewAddress != string.Empty)
                {
                    Address addressToAdd = new Address() { Address1 = model.NewAddress };
                    //Add new address to address repo
                    addressRepo.Insert(addressToAdd);
                    addressRepo.Save();
                    currentUser.Addresses.Add(addressToAdd);
                    userRepo.Save();
                    addressId = addressToAdd.AddressId;
                }
            }
            else
            {
                try
                {
                    int.TryParse(model.Address, out addressId);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (addressId >= 1)
            {
                order = new Order()
                {
                    ClientId = currentUser.Id,
                    OrderTime = orderTime,
                    DeliveryAddressId = addressId,
                    OrderDeliverd = false,
                    OrderPrice = ((Cart)Session["Cart"]).TotalPrice(),
                    OrderPrepared = false,
                    IsActive = true,
                    ExtraSpecifications = model.ExtraSpecifications,
                };

                //We add the order to the database.
                orderRepo.Insert(order);
                orderRepo.Save();

                //We add the order reference to each of the dishes.
                foreach (CartLine line in ((Cart)Session["Cart"]).Lines)
                {
                    //We add the order reference to the dishes in the 
                    //cart session to ensure the many to many relationship.
                    var currentDish = dishRepo.GetById(line.Dish.DishId);
                    currentDish.Orders.Add(order);
                    orderRepo.Save();
                }
                orderRepo.Save();
                dishRepo.Save();

                verifyInsertion = orderRepo.GetById(order.OrderId);

                Debug.WriteLine($"{order.Dishes.Count()}");
            }
            else
                return RedirectToAction("CompleteCart");

            //check if order got registered.
            if (verifyInsertion != null)
            {
                return RedirectToAction("Completed", new { orderId = order.OrderId });
            }
            else
            {
                return RedirectToAction("CompleteCart");
            }
        }

        //GET: Ceckout/Completed
        [HttpGet]
        public ActionResult Completed(int orderId = -1)
        {
            Order order = orderRepo.GetById(orderId);
            Cart cart = (Cart)Session["Cart"];
            cart.ClearCart();
            return View(order);
        }

        //GET: Checkout/CancelOrder
        [HttpGet]
        public ActionResult CancelOrder(int orderToCancelId)
        {
            Order order = orderRepo.GetById(orderToCancelId);
            if (order != null)
            {
                order.IsActive = false;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
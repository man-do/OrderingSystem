using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingSystem.WebApp.Models;
using OrderingSystem.Data.Data.Abstract;

namespace OrderingSystem.WebApp.Controllers
{
    //ToDo: Auhtorize
    public class CartController : Controller
    {
        private IRepository<Dish> dishRepo;

        public CartController(IRepository<Dish> dishRepoParam) => dishRepo = dishRepoParam;

        // GET: Cart
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            CartIndexViewModel cartIndex = new CartIndexViewModel()
            {
                Cart = this.GetCart(),
                ReturnUrl = returnUrl
            };

            return View(cartIndex);
        }
        

        //POST - Add to cart
        public RedirectToRouteResult AddToCart(int dishIdParam , string returnUrl)
        {
            Dish dish = dishRepo.GetAll().FirstOrDefault(d => d.DishId == dishIdParam);

            if (dish != null)
            {
                GetCart().AddDish(dish, 1);
            }

            return RedirectToAction("Index", "Home" , new { returnUrl = returnUrl });
        }

        //POST - Remove from cart
        public RedirectToRouteResult RemoveFromCart(int dishIdParam , string returnUrl)
        {
            Dish dish = dishRepo.GetById(dishIdParam);

            if (dish != null)
            {
                GetCart().RemoveLine(dish);
            }
            
            return RedirectToAction("Index" , "Cart" ,  new { returnUrl});
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}
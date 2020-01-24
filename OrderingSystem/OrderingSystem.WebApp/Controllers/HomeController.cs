using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Concrete;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Common;
using OrderingSystem.WebApp.Models;

namespace OrderingSystem.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Dish> dishRepo = null;
        private IRepository<Igredient> igredientRepo = null;

        public HomeController(IRepository<Dish> dishRepoParam, IRepository<Igredient> igredientRepoParam)
        {
            dishRepo = dishRepoParam;
            igredientRepo = igredientRepoParam;
        }

        // GET- Home/Index
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(
            string searchPhrase = null,
            string dishType = null,
            string allergy = null,
            decimal rating = 0,
            decimal igredientId = -1,
            int page = 0,
            string returnUrl = null)
        {
            ListDishesViewModel listDishesViewModel = new ListDishesViewModel();
            IEnumerable<Dish> result = new List<Dish>();
            PagingInfo pagingInfo = new PagingInfo();
            // Change this to change page capacity
            const int pageCapacity = 5;

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // Filter - search phrase
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                result = dishRepo.GetAll()
                    .Where(d => d.Name.Trim()
                    .ToLower()
                    .Contains(searchPhrase.Trim()
                    .ToLower())
                    || d.Description.Trim().ToLower()
                    .Contains(searchPhrase.Trim().ToLower()));
            }
            // Filter - dish type
            else if (!string.IsNullOrWhiteSpace(dishType))
            {
                switch (dishType.Trim().ToLower())
                {
                    case "pescatarian":
                        result = dishRepo.GetAll().Where(d => d.IsPescatarian);
                        break;
                    case "vegan":
                        result = dishRepo.GetAll().Where(d => d.IsVegan);
                        break;
                    case "vegetarian":
                        result = dishRepo.GetAll().Where(d => d.IsVegetarian);
                        break;
                    case "seafood":
                        result = dishRepo.GetAll().Where(d => d.HasSeafood);
                        break;
                    default:
                        result = dishRepo.GetAll();
                        break;
                }
            }
            // Filter - allergy
            else if (!string.IsNullOrWhiteSpace(allergy))
            {
                switch (allergy.Trim().ToLower())
                {
                    case "peanuts":
                        result = dishRepo.GetAll().Where(d => d.HasPeanuts == false);
                        break;
                    case "seafood":
                        result = dishRepo.GetAll().Where(d => d.HasSeafood == false);
                        break;
                    default:
                        result = dishRepo.GetAll();
                        break;
                }
            }
            // Filter - rating
            else if (rating > 0)
            {
                switch (rating)
                {
                    case 1:
                        result = dishRepo.GetAll().Where(d => d.Rating > 1);
                        break;
                    case 2:
                        result = dishRepo.GetAll().Where(d => d.Rating > 2);
                        break;
                    case 3:
                        result = dishRepo.GetAll().Where(d => d.Rating > 3);
                        break;
                    case 4:
                        result = dishRepo.GetAll().Where(d => d.Rating > 4);
                        break;
                    default:
                        result = dishRepo.GetAll();
                        break;
                }
            }
            // Filter - ingredient
            else if (igredientId != -1)
            {
                var igredient = igredientRepo.GetById(igredientId);
                result = igredient.Dishes;
            }
            else
            {
                result = dishRepo.GetAll();
            }

			result = result.Where(d => d.Enabled == true);
            listDishesViewModel.Dishes = result.Skip(page * pageCapacity).Take(pageCapacity).OrderBy(d => d.Name).ToList();
            listDishesViewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = pageCapacity,
                TotalItems = result.Count()

            };

            if (listDishesViewModel.PagingInfo.TotalPages < page || page < 0)
            {
                return View("Error", new HandleErrorInfo(new Exception("Page not found"), "Home", "Index"));
            }
            else
                return View(listDishesViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Filter()
        {
            return PartialView(igredientRepo.GetAll());
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [NonAction]
        public bool ContainsIgredient(IEnumerable<Igredient> igredientList, Igredient igredientParam)
        {
            foreach (Igredient igredient in igredientList)
            {
                if (igredient == igredientParam)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Concrete;
using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingSystem.WebApp.Models;

namespace OrderingSystem.WebApp.Controllers
{
	public class ChefController : Controller
	{
		OSEntities context = new OSEntities();

		private IRepository<Dish> dishRepo;
		private IRepository<Igredient> igredientRepo;
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ChefController()
		{
			dishRepo = new DbRepository<Dish>(context);
			igredientRepo = new DbRepository<Igredient>(context);
		}
		
		[Authorize(Roles = "Chef")]
		public ActionResult Index()
		{
			return View();
		}

		// Dish list partial
		[Authorize(Roles = "Chef")]
		[HttpGet]
		public PartialViewResult DishesByFilterPartial(string id, string search)
		{
			IEnumerable<Dish> result = null;

			try
			{
				if (id == "Vegan")
				{
					result = dishRepo.GetAll().Where(d => d.IsVegan == true);
				}
				else
					if (id == "Vegetarian")
				{
					result = dishRepo.GetAll().Where(d => d.IsVegetarian == true);
				}
				else
					if (id == "Pescatarian")
				{
					result = dishRepo.GetAll().Where(d => d.IsPescatarian == true);
				}
				else
					if (id == "Has peanut")
				{
					result = dishRepo.GetAll().Where(d => d.HasPeanuts == true);
				}
				else
					if (id == "Has seafood")
				{
					result = dishRepo.GetAll().Where(d => d.HasSeafood == true);
				}
				else
					result = dishRepo.GetAll();

				if (!string.IsNullOrWhiteSpace(search))
				{
					//result = result.Where(d => d.Name.ToLowerInvariant().StartsWith(search.ToLowerInvariant()));
					result = result.Where(d => d.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()));
				}
			}
			catch (Exception e)
			{
				log.Error(e.Message);
			}
			return PartialView(result);
		}

		// Chef/CreateDish
		[Authorize(Roles = "Chef")]
		public ActionResult CreateDish()
		{
			try
			{
				var allIgrendients = igredientRepo.GetAll();

				var newDish = new NewDishViewModel();

				var igredientCheckboxmodels = new List<IgredientCheckboxViewModel>();
				foreach (var igredient in allIgrendients)
				{
					igredientCheckboxmodels.Add(new IgredientCheckboxViewModel
					{
						IgredientId = igredient.IngredientIDd,
						Name = igredient.Name,
						IsChecked = false
					});
				}

				newDish.Igredients = igredientCheckboxmodels;
				return View("SaveDish", newDish);
			}
			catch (Exception e)
			{
				log.Error(e.Message);
				return View("Error", new HandleErrorInfo(new Exception("Page not Found"), "Chef", "CreateDish"));
			}
		}

		// Save dish or changes to dish
		[Authorize(Roles = "Chef")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(NewDishViewModel newDish, HttpPostedFileBase image1)
		{
			if (ModelState.IsValid)
			{
				// Checking if the dish alredy exists
				if (newDish.DishId == 0)
				{
					var dish = new Dish();
					dish.Name = newDish.Name;
					dish.Description = newDish.Description;
					dish.Price = newDish.Price;
					dish.IsPescatarian = newDish.IsPescatarian;
					dish.IsVegan = newDish.IsVegan;
					dish.IsVegetarian = newDish.IsVegetarian;
					dish.HasPeanuts = newDish.HasPeanuts;
					dish.HasSeafood = newDish.HasSeafood;
					dish.Enabled = true;

					// Seting up the image as byte array
					if (image1 != null)
					{
						dish.Image = new byte[image1.ContentLength];
						image1.InputStream.Read(dish.Image, 0, image1.ContentLength);
					}

					foreach (var item in newDish.Igredients)
					{
						if (item.IsChecked == true)
						{
							// Same as below
							Igredient ig = igredientRepo.GetById(item.IgredientId);
							dish.Igredients.Add(ig);
						}
					}
					try
					{
						dishRepo.Insert(dish);
						log.Info("New dish named " + newDish.Name + " was created");
					}
					catch (Exception e)
					{
						log.Error(e.Message);
						return View("Error", new HandleErrorInfo(new Exception("Dish not saved"), "Chef", "Save"));
					}
				}
				else
				{
					// If dish already exists in database
					var dishInDb = dishRepo.GetById(newDish.DishId);
					dishInDb.Name = newDish.Name;
					dishInDb.Description = newDish.Description;
					dishInDb.Price = newDish.Price;
					dishInDb.IsPescatarian = newDish.IsPescatarian;
					dishInDb.IsVegan = newDish.IsVegan;
					dishInDb.IsVegetarian = newDish.IsVegetarian;
					dishInDb.HasPeanuts = newDish.HasPeanuts;
					dishInDb.HasSeafood = newDish.HasSeafood;
					dishInDb.Enabled = true;
					if (image1 != null)
					{
						dishInDb.Image = new byte[image1.ContentLength];
						image1.InputStream.Read(dishInDb.Image, 0, image1.ContentLength);
					}

					// Reset ingredients i the existing dish
					dishInDb.Igredients.Clear();
					foreach (var item in newDish.Igredients)
					{
						if (item.IsChecked == true)
						{
							// Should not create new igredients but take those from db
							Igredient ig = igredientRepo.GetById(item.IgredientId);
							dishInDb.Igredients.Add(ig);
						}
					}
					log.Info("Dishish named " + dishInDb.Name + " was edited");
				}

				try
				{
					dishRepo.Save();
					return RedirectToAction("Index", "Chef");
				}
				catch (Exception e)
				{
					log.Error(e.Message);
					return View("Error", new HandleErrorInfo(new Exception("Dish not saved"), "Chef", "Save"));
				}
			}
			else
			{
				// In case of not valid form data
				return View("SaveDish", newDish);
			}
		}

		// Edit dish
		[Authorize(Roles = "Chef")]
		public ActionResult Edit(int id)
		{
			Dish dish = null;
			try
			{
				dish = dishRepo.GetById(id);
			}
			catch (Exception e)
			{
				log.Error(e.Message);
				return View("Error", new HandleErrorInfo(new Exception("Dish not found"), "Chef", "Edit"));
			}
			if (dish != null)
			{
				NewDishViewModel dishViewModel = new NewDishViewModel();
				dishViewModel.DishId = dish.DishId;
				dishViewModel.Name = dish.Name;
				dishViewModel.Price = dish.Price;
				dishViewModel.IsPescatarian = dish.IsPescatarian;
				dishViewModel.IsVegan = dish.IsVegan;
				dishViewModel.IsVegetarian = dish.IsVegetarian;
				dishViewModel.HasPeanuts = dish.HasPeanuts;
				dishViewModel.HasSeafood = dish.HasSeafood;
				dishViewModel.Description = dish.Description;

				List <IgredientCheckboxViewModel> igredientCheckboxesList = new List<IgredientCheckboxViewModel>();
				var igrendientList = igredientRepo.GetAll().ToList();
				foreach (Igredient igredient in igrendientList)
				{
					IgredientCheckboxViewModel igredientCheckboxViewModel = new IgredientCheckboxViewModel();
					igredientCheckboxViewModel.IgredientId = igredient.IngredientIDd;
					igredientCheckboxViewModel.Name = igredient.Name;

					// Problem with iterating
					foreach (Igredient ig in dish.Igredients)
					{
						if (ig.IngredientIDd == igredientCheckboxViewModel.IgredientId)
						{
							igredientCheckboxViewModel.IsChecked = true;
							break;
						}
					}
					igredientCheckboxesList.Add(igredientCheckboxViewModel);
				}

				dishViewModel.Igredients = igredientCheckboxesList;
				dishViewModel.Image = dish.Image;

				return View("SaveDish", dishViewModel);
			}
			else
			{
				return HttpNotFound();
			}
		}

		// Enable/Disable dish
		[Authorize(Roles = "Chef")]
		public ActionResult EnableDisableDish(int id)
		{
			var dishInDb = dishRepo.GetById(id);

			if (dishInDb != null)
			{
				dishInDb.Enabled = !dishInDb.Enabled;
				dishRepo.Save();
			}

			return View("Index", dishRepo.GetAll());
		}
	}
}
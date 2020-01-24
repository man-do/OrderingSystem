using Microsoft.AspNet.Identity;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Concrete;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Business;
using OrderingSystem.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderingSystem.Data.Common;

namespace OrderingSystem.WebApp.Controllers
{
	public class KitchenStaffController : Controller
	{
		private OSEntities context = new OSEntities();
		private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private IRepository<Order> orderRepo;
		private IRepository<Address> addressRepo;
		private IRepository<AspNetUser> userRepo;
		private IRepository<Staff_Capacity> staffRepo;

		public KitchenStaffController()
		{
			orderRepo = new DbRepository<Order>(context);
			addressRepo = new DbRepository<Address>(context);
			userRepo = new DbRepository<AspNetUser>(context);
			staffRepo = new DbRepository<Staff_Capacity>(context);
		}

		// GET: All orders
		[Authorize(Roles = "KitchenStaff")]
		public ActionResult AllOrders(int page = 0)
		{
			var allOrders = orderRepo.GetAll().ToList();
			// Paggination
			const int pageCapacity = 10;
			int ordersCount = allOrders.Count;
			int maxPage = (int)Math.Ceiling((decimal)(ordersCount / pageCapacity));
			if (page < 0)
			{
				page = 0;
			}
			else if(page > maxPage)
			{
				page = maxPage;
			}
			allOrders = allOrders.Skip(page * pageCapacity).Take(pageCapacity).ToList();

			// Mapping
			AllOrdersViewModel allOrdersView = new AllOrdersViewModel();
			allOrdersView.OrdersModels = new List<OrderViewModel>();
			foreach (var order in allOrders)
			{
				OrderViewModel orderView = new OrderViewModel();
				orderView.OrderId = order.OrderId;
				orderView.OrderTime = order.OrderTime;
				orderView.DeliveryAddress = addressRepo.GetById(order.DeliveryAddressId).Address1;
				orderView.OrderPrice = order.OrderPrice;
				allOrdersView.OrdersModels.Add(orderView);
			}
			allOrdersView.PagingInfo = new PagingInfo()
			{
				CurrentPage = page,
				ItemsPerPage = pageCapacity,
				TotalItems = ordersCount
			};

			return View(allOrdersView);
		}

		// Assign orders to this kitchen staff user
		[Authorize(Roles = "KitchenStaff")]
		public ActionResult MyOrders() // Add int id
		{
			// Assign orders to kitchen staff
			AssignHelper assignHelper = new AssignHelper(staffRepo, orderRepo);
			var result = orderRepo.GetAll()
				.Where(o => o.IsActive == true && o.KitchenStaffId == null && (DateTime.Now - o.OrderTime).TotalMinutes > 5).ToList();
			assignHelper.AssignKitchenStaff(result);

			_UserViewModel currentUser = new _UserViewModel();
			currentUser.UserId = User.Identity.GetUserId();
			log.Info(User.Identity.GetUserId() + " " + "was disabled");
			return View("MyOrders", currentUser);
		}

		[Authorize(Roles = "KitchenStaff")]
		[HttpGet]
		public PartialViewResult MyOrdersList(string id)
		{
			var myOrders = orderRepo.GetAll()
				.Where(o => o.KitchenStaffId == id && o.OrderPrepared == false)
				.OrderBy(o => o.OrderTime)
				.ToList();
			List<OrderViewModel> myOrdersView = new List<OrderViewModel>();
			foreach (var order in myOrders)
			{
				OrderViewModel orderView = new OrderViewModel();
				orderView.OrderId = order.OrderId;
				orderView.OrderPrepared = order.OrderPrepared;
				orderView.Dishes = order.Dishes;
				myOrdersView.Add(orderView);
			}
			return PartialView("MyOrdersListpartial", myOrdersView);
		}

		[Authorize(Roles = "KitchenStaff")]
		[HttpGet]
		public ActionResult OrderDetails(int id)
		{
			var result = orderRepo.GetById(id);
			OrderViewModel order = new OrderViewModel();
			order.OrderId = result.OrderId;
			order.OrderPrepared = result.OrderPrepared;
			order.Dishes = result.Dishes;
			order.ExtraSpecs = result.ExtraSpecifications;

			return View(order);
		}

		[Authorize(Roles = "KitchenStaff")]
		public ActionResult OrderPrepared(int id)
		{
			try
			{
				var result = orderRepo.GetById(id);
				if (result != null)
				{
					result.OrderPrepared = true;
					orderRepo.Update(result);
					orderRepo.Save();
					_UserViewModel currentUser = new _UserViewModel();
					currentUser.UserId = User.Identity.GetUserId();
					return View("MyOrders", currentUser);
				}
				else
				{
					return HttpNotFound();
				}
			}
			catch (Exception ex)
			{
				string exeptionMessaage = ex.Message;
				return new HttpStatusCodeResult(500);
			}
		}
	}
}
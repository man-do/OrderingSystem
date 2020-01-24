using Microsoft.AspNet.Identity;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingSystem.WebApp.Controllers
{
	public class TaxiStaffController : Controller
	{
		IRepository<Order> orderRepo;
		IRepository<Address> addressRepo;
		IRepository<AspNetUser> userRepo;

		public TaxiStaffController(IRepository<Order> orderRepoParam, IRepository<Address> addressRepoParam, IRepository<AspNetUser> userRepoParam)
		{
			orderRepo = orderRepoParam;
			addressRepo = addressRepoParam;
			userRepo = userRepoParam;
		}

		// GET: TaxiStaff
		[Authorize(Roles = "Taxi")]
		public ActionResult Index()
		{
			try
			{
				var allOrders = orderRepo.GetAll()
						.Where(o => o.OrderPrepared == true && o.OrderDeliverd == false && o.IsActive == true && (DateTime.Now - o.OrderTime).Hours < 10)
						.OrderBy(o => o.OrderTime);

				List<OrderViewModel> orderView = new List<OrderViewModel>();
				foreach (Order order in allOrders)
				{
					OrderViewModel vmOrder = new OrderViewModel();
					vmOrder.clientUserName = userRepo.GetById(order.ClientId).UserName;
					vmOrder.OrderId = order.OrderId;
					vmOrder.OrderPrepared = true;
					vmOrder.OrderTime = order.OrderTime;
					vmOrder.OrderPrice = order.OrderPrice;
					vmOrder.OrderDeliverd = order.OrderDeliverd;
					vmOrder.DeliveryAddress = addressRepo.GetById(order.DeliveryAddressId).Address1;
					orderView.Add(vmOrder);
				}

				return View(orderView);
			}
			catch (Exception e)
			{
				string exeption = e.Message;
				return new HttpStatusCodeResult(500);
			}
		}

		[Authorize(Roles = "Taxi")]
		public ActionResult OrderGet(int id)
		{
			try
			{
				var order = orderRepo.GetById(id);
				if (order.TaxiDriverId == null)
				{
					OrderViewModel orderView = new OrderViewModel();
					orderView.clientUserName = userRepo.GetById(order.ClientId).UserName;
					orderView.DeliveryAddress = addressRepo.GetById(order.DeliveryAddressId).Address1;
					orderView.Dishes = order.Dishes;
					orderView.OrderPrice = order.OrderPrice;
					orderView.OrderTime = order.OrderTime;
					orderView.TaxiDriverId = order.TaxiDriverId;
					orderView.OrderId = order.OrderId;

					// Order details view
					return View(orderView);
				}
				else
				{
					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				string ex = e.Message;
				return new HttpStatusCodeResult(500);
			}
		}

		[Authorize(Roles = "Taxi")]
		public ActionResult OrderAccepted(int id)
		{
			try
			{
				var result = orderRepo.GetById(id);
				if (result.TaxiDriverId == null)
				{
					result.TaxiDriverId = User.Identity.GetUserId();
					orderRepo.Save();

					OrderViewModel orderView = new OrderViewModel();
					orderView.clientUserName = userRepo.GetById(result.ClientId).UserName;
					orderView.DeliveryAddress = addressRepo.GetById(result.DeliveryAddressId).Address1;
					orderView.Dishes = result.Dishes;
					orderView.OrderPrice = result.OrderPrice;
					orderView.OrderTime = result.OrderTime;
					orderView.TaxiDriverId = result.TaxiDriverId;
					orderView.OrderId = result.OrderId;

					return View("OrderGet", orderView);
				}
				else
				{
					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				string ex = e.Message;
				return new HttpStatusCodeResult(500);
			}
		}

		[Authorize(Roles = "Taxi")]
		public ActionResult OrderDelivered(int id)
		{
			try
			{
				var result = orderRepo.GetById(id);
				result.OrderDeliverd = true;
				orderRepo.Save();
				return RedirectToAction("Index");
			}
			catch (Exception e)
			{
				string ex = e.Message;
				return new HttpStatusCodeResult(500);
			}
		}
	}
}
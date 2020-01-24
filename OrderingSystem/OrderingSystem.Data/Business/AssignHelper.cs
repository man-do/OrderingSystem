using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Business
{
	public struct StaffDifference
	{
		public string staffId;
		public int difference;
	}

	public class AssignHelper
	{
		IRepository<Staff_Capacity> staffRepo;
		IRepository<Order> orderRepo;

		public AssignHelper(IRepository<Staff_Capacity> staffrepoParam, IRepository<Order> orderRepoParam)
		{
			staffRepo = staffrepoParam;
			orderRepo = orderRepoParam;
		}

		// Method returns a staffId string or an empty string if errors a orcurs
		public void AssignKitchenStaff(List<Order> theOrders)
		{
			try
			{
				int currentOrderNumber;
				List<StaffDifference> staffDifferencesList = new List<StaffDifference>();

				var staffMembers = staffRepo.GetAll().ToList();
				// Getting the diffeences betteen stafs member max capacity and ther current order number
				foreach (var member in staffMembers)
				{
					currentOrderNumber = orderRepo.GetAll().Where(o => o.KitchenStaffId == member.UserId && o.IsActive == true && o.OrderPrepared == false).Count();
					StaffDifference staffDifference;
					staffDifference.staffId = member.UserId;
					staffDifference.difference = member.Capacity - currentOrderNumber;
					staffDifferencesList.Add(staffDifference);
				}

				foreach (Order order in theOrders)
				{
					if (staffDifferencesList.Count > 1)
					{
						if (staffDifferencesList[0].difference < staffDifferencesList[1].difference)
						{
							staffDifferencesList.OrderByDescending(o => o.difference);
						}
					}
					order.KitchenStaffId = staffDifferencesList[0].staffId;
					orderRepo.Save();
				}

			}
			catch (Exception ex)
			{
				string e = ex.Message;
			}
		}
	}
}

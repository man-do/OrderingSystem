using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingSystem.WebApp.Models
{
    public class CheckoutViewModel
    {
        private string googleMapCoordinates;

        public string Address { get; set; }

        public string NewAddress { get; set; }

        public string ExtraSpecifications { get; set; }

        public IEnumerable<Address> AllUserAddresses { get; set; }

        public IEnumerable<SelectListItem> GetAddressesForDropDown (IEnumerable<Address> addressesParam)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (Address address in addressesParam)
            {
                var newElement = new SelectListItem() { Text = address.Address1, Value = address.AddressId.ToString() };
                selectList.Add(newElement);
            }

            return selectList;
        }

        //TODO - implement logic for bonus
        public string GoogleMapCoordinates
        {
            get => this.googleMapCoordinates;
            set => googleMapCoordinates = value;
        }
    }
}
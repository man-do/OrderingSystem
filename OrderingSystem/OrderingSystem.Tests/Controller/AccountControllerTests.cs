using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderingSystem.WebApp.Controllers;
using OrderingSystem.WebApp.Models;

namespace OrderingSystem.Tests.Controller
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Redirect_To_Action_Based_On_Role()
        {
            var model = new LoginViewModel()
            {
                Email = "landia@gmail.com",
                Password = "Landia@1234",
                RememberMe = false
            };

            var cont = new AccountController();

            var result = cont.Login(model, null).Result as RedirectToRouteResult;

            Assert.IsTrue(result != null);
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }
    }
}

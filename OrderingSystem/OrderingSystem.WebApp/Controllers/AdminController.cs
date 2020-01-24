using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OrderingSystem.Data.Business.Abstract;
using OrderingSystem.Data.Business.Concrete;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Concrete;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Data.Models;
using OrderingSystem.WebApp.Models;
using PagedList;
using PagedList.Mvc;

namespace OrderingSystem.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService = null;
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AdminController(IUserService userService, IRepository<AspNetRole> repository)
        {
            _userService = userService;
        }

        // GET: Admin
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int page)
        {
            try
            {
                IEnumerable<UserViewModel> result = _userService.GetAll("").ToPagedList((page == 0) ? 1 : page, 10);
                return View(result);
            }
            catch(Exception e)
            {
                log.Error(e.Message);
            }
            return View("Error",new HandleErrorInfo(new Exception("Page not Found"),"Admin","Index"));
        }
        // GET: Admin
        [Authorize(Roles = "Administrator")]
        public ActionResult SearchUser(string search)
        {
            try
            {
                IEnumerable<UserViewModel> result = _userService.GetAll(search);
                return PartialView("UserPartial", result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GetDisabled()
        {
            IEnumerable<UserViewModel> result = _userService.GetDisabled();
            return PartialView("UserPartial", result);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Disable (string Id)
        {
            _userService.Disable(Id);
            UserViewModel user = _userService.GetbyId(Id);
            log.Info(user.Email + " " + "was disabled");
            return RedirectToAction("Index", "Admin");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Enable(string Id)
        {
            _userService.Enable(Id);
            UserViewModel user = _userService.GetbyId(Id);
            log.Info(user.Email + " " + "was enabled");
            return RedirectToAction("Index", "Admin");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Details (string Id)
        {
            UserViewModel userViewModel = _userService.GetbyId(Id);
            return View(userViewModel);
        }

        //GET
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string Id)
        {
            UserViewModel userViewModel = _userService.GetbyId(Id);
            return View(userViewModel);
        }

        //POST
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit (UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _userService.Edit(userViewModel);

                return RedirectToAction("EditConfirmation", "Account", new { userId = userViewModel.Id, email = userViewModel.Email, username = userViewModel.UserName });
            }
            return RedirectToAction("Index");
        }

        //GET
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult ResetPassword(string Id)
        {
            UserViewModel userViewModel = _userService.GetbyId(Id);
            return View(userViewModel);
        }

        //POST
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult ResetPassword (UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("AdminResetPassword","Account",new { userid=userViewModel.Id, passw=userViewModel.Password});
            }
            return RedirectToAction("Index");
        }

    }
}
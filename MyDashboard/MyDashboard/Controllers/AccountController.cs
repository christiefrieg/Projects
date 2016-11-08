using Dashboard.DataServices;
using Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult ManageAccount()
        {
            var accountView = new AccountViewModel();

            return View(accountView);
        }

        public ActionResult Login(string url)
        {
            var loginView = new LoginViewModel();
            loginView.RedirectUrl = url;
            return View(loginView);
        }
        public ActionResult LogInUser(string username, string password)
        {
            if (UserDataService.LogInUser(username, password))
                return Json("success");
            else
                return Json("error");
        }
        [HttpGet]
        public ActionResult GetCurrentUser()
        {
            return Json(UserDataService.CurrentUser, JsonRequestBehavior.AllowGet);
        }
    }
}
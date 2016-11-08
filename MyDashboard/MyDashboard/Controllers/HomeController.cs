using Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            if (HttpContext.Session["username"] != null)
                homeViewModel.UserLoggedIn = true;
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

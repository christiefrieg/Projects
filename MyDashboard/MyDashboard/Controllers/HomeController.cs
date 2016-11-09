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
            var encodedUrl = HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Home/Dashboard"); //replace IP with Request.Url.Authority           
            var homeViewModel = new HomeViewModel();
            if (HttpContext.Session["username"] != null)
                homeViewModel.UserLoggedIn = true;
            homeViewModel.RedirectUri = encodedUrl;
            return View(homeViewModel);
        }

        public ActionResult Dashboard(string code, string state, string error)
        {
            var encodedUrl = HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Home/Dashboard");
            var dashboardVM = new DashboardViewModel(encodedUrl);
            if (HttpContext.Session["username"] != null)
                dashboardVM.UserLoggedIn = true;
            dashboardVM.InitializePanel(code, state, error);
            return View(dashboardVM);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

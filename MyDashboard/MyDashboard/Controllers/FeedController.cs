
using Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class FeedController : Controller
    {
        public ActionResult ManageFeeds()
        {
            var feedViewModel = new FeedViewModel();
            return View(feedViewModel);
        }
    }
}
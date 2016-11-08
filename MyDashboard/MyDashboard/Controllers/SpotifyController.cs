using Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class SpotifyController : Controller
    {
        public ActionResult Index()
        {
            var spotifyView = new SpotifyViewModel();
            return View(spotifyView);
        }
    }
}
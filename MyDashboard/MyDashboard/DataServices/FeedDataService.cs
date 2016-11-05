using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.DataServices
{
    public class FeedDataService
    {
        public static List<Feed> GetFeedsForCurrentUser()
        {
            int userId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            return new List<Feed>();
        }
    }
}
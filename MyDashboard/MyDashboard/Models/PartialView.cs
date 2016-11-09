using Dashboard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class PartialView
    {
        public string Title { get; set; }
        public dynamic ViewModel { get; set; }
        public string PartialViewPath { get; set; }

    }
}
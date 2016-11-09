using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel(string uri)
        {
            RedirectUri = uri;
        }
        public string RedirectUri { get; set; }
    }
}
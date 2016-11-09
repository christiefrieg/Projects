using Dashboard.DataServices;
using Dashboard.Interfaces;
using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
    public class DashboardViewModel
    {
        public bool UserLoggedIn { get; set; }
        public List<PartialView> PartialViews { get; set; }

        public string Title { get; set; }
        public string RedirectUri { get; set; }
        public string PartialViewPath { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public string Error { get; set; }
        public void InitializePanel(string code, string state, string error)
        {
            Code = code;
            State = state;
            Error = error;
            var token = SpotifyDataService.GetToken(code, RedirectUri);
            token.Wait();
            var views = new List<PartialView>();
            var spotifyVM = new SpotifyViewModel();
            spotifyVM.InitializeView(token.Result.access_token);
            views.Add(new PartialView
            {
                PartialViewPath = spotifyVM.PartialViewPath,
                Title = spotifyVM.Title,
                ViewModel = spotifyVM
            });
            var feedVM = new FeedViewModel();
            feedVM.InitializeView();
            views.Add(new PartialView
            {
                PartialViewPath = feedVM.PartialViewPath,
                Title = feedVM.Title,
                ViewModel = feedVM
            });
            PartialViews = views;

        }
        public DashboardViewModel(string uri)
        {
            RedirectUri = uri;
            PartialViews = new List<PartialView>();
        }
    }
}
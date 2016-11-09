using Dashboard.DataServices;
using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
    public class SpotifyViewModel : PartialView
    {
        public List<Album> NewReleases { get; set; }
        public void InitializeView(string token)
        {
            NewReleases = SpotifyDataService.GetNewReleases(token);
            PartialViewPath = "~/Views/PartialView/_SpotifyPartialView.cshtml";
            Title = "Spotify";
        }
    }
}
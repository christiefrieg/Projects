using Dashboard.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
    public class SpotifyViewModel
    {
        public List<Album> NewReleases
        {
            get
            {
                return SpotifyDataService.GetNewReleases();
            }
        }
    }
}
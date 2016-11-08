using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


namespace Dashboard.DataServices
{
    public class SpotifyDataService
    {
        private static string NewReleases = "https://api.spotify.com/v1/browse/new-releases";
        private static string Album = "https://api.spotify.com/v1/albums/{0}";
        private static string Albums = "https://api.spotify.com/v1/albums?ids={0}";
        private static string AlbumTracks = "https://api.spotify.com/v1/albums/{0}/tracks";
        static HttpClient client = new HttpClient();
        private static List<SpotifyArtist> GetArtists()
        {
            var user = UserDataService.CurrentUser;
            if (user != null)
            {
                using (var db = new PortfolioEntities())
                {
                    return db.SpotifyArtists.Where(sa => sa.UserID == user.UserID).ToList();
                }
            }
            else
                return null;
        }
        public static List<Album> GetNewReleases()
        {
            var user = UserDataService.CurrentUser;
            var newAlbums = new List<Album>();
            if (user != null)
            {
                using (var db = new PortfolioEntities())
                {
                    var artists = GetArtists();
                    var releases = GetNewReleasesTask();
                    foreach(var album in releases.Result)
                    {
                        foreach(var artist in album.artists)
                            foreach(var favArtist in artists)
                                if(artist == favArtist.ArtistName)
                                    newAlbums.Add(new Album {
                                        AlbumID = Convert.ToInt32(album.id),
                                        AlbumName = album.name,
                                        Image = album.images[0].url,
                                        Link = album.href,
                                        ReleaseDate = DateTime.Now,
                                        SpotifyArtist = favArtist,
                                        SpotifyArtistID = favArtist.SpotifyArtistID
                                    });
                    }
                }
                return newAlbums;
            }
            else
                return null;
        }
        private static async Task<List<AlbumObject>> GetNewReleasesTask()
        {           
            var albumList = new List<AlbumObject>();
            HttpResponseMessage response = await client.GetAsync(NewReleases);
            if (response.IsSuccessStatusCode)
            {
                albumList = await response.Content.ReadAsAsync<List<AlbumObject>>();
            }
            return albumList;
        }
    }
}
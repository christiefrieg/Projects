using Dashboard.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private static string SpotifyToken = "https://accounts.spotify.com/api/token";
        private static string ClientID = "92388574f7cd4031bb2f8176a47d45e1";
        private static string ClientSecret = "fd2f38f13bbb4fb7a6fbc5a3d659edfc";
        private static string Token;
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
        public static List<Album> GetNewReleases(string token)
        {
            var user = UserDataService.CurrentUser;
            var newAlbums = new List<Album>();
            if (user != null)
            {
                using (var db = new PortfolioEntities())
                {
                    var artists = GetArtists();
                    var releases = GetNewReleasesTask(token);
                    releases.Wait();
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
        public static async Task<SpotifyTokenResponse> GetToken(string code, string uri)
        {
            var tokenResponse = new SpotifyTokenResponse();
            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    // HTTP POST
            //    var request = new SpotifyTokenRequest()
            //    {
            //        client_id = ClientID,
            //        client_secret = ClientSecret,
            //        code = code,
            //        grant_type = "authorization_code",
            //        redirect_uri = uri
            //    };
            //    HttpResponseMessage response = await client.PostAsJsonAsync(SpotifyToken, JsonConvert.SerializeObject(request));
            //    if (response.IsSuccessStatusCode)
            //    {
            //        tokenResponse = await response.Content.ReadAsAsync<SpotifyTokenResponse>();
            //    }
            //}
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(SpotifyToken);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var request = new SpotifyTokenRequest()
                {
                    client_id = ClientID,
                    client_secret = ClientSecret,
                    code = code,
                    grant_type = "authorization_code",
                    redirect_uri = uri
                };
                string json = JsonConvert.SerializeObject(request);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            //Token = tokenResponse.access_token;
            return tokenResponse;
        }
        private static async Task<List<AlbumObject>> GetNewReleasesTask(string token)
        {           
            var albumList = new List<AlbumObject>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.GetAsync(NewReleases);
                if (response.IsSuccessStatusCode)
                {
                    albumList = await response.Content.ReadAsAsync<List<AlbumObject>>();
                }
            }
            return albumList;
        }
    }
}
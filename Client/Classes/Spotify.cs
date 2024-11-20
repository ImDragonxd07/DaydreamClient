using Il2CppNewtonsoft.Json;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Daydream.Client.Classes
{
    internal class Spotify

    {
        class AccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public long expires_in { get; set; }
        }
        static async Task<AccessToken> GetToken()
        {
            Utility.Logger.networklog("Getting Token");
            string clientId = "a6330824f6804ec5b0b6c125c7c0eca3";
            string clientSecret = "ffd1d3cb9adb4233a842bfa4eef074c1";
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            using (var client = new HttpClient())
            {
                //Define Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                var response = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccessToken>(response);
            }
        }
        public static async Task Main()
        {
            AccessToken token = GetToken().Result;

            Utility.Logger.networklog("Spotify access token " + token.access_token);
            var spotify = new SpotifyClient(token.access_token);
            var track = await spotify.Tracks.Get("1s6ux0lNiTziSrd7iUAADH");
            Utility.Logger.networklog("Spotify track " + track.Name);

        }
    }
}

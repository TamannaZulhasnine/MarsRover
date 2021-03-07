using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarsRover.Interfaces;
using MarsRover.Models;
using Newtonsoft.Json;
using NLog;

namespace MarsRover
{
    public class MarsRoverApiManager : IRoverApiManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string uristring = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/";

        public async void GetPhotosAsync(string date, string key)
        {
            var datePhoto = GetDate(date);

            if (!string.IsNullOrEmpty(datePhoto))
            {
                var uriPath = GetUriPath(uristring, datePhoto, key);

                var response = GetResponseAsync(uriPath).Result;

                if (!string.IsNullOrEmpty(response))
                {
                    var data = JsonConvert.DeserializeObject<Photos>(response);

                    foreach (var photo in data.photos)
                    {
                        var imageUri = new Uri(photo.Img_src);
                        using (var webclient = new WebClient())
                        {
                            Directory.CreateDirectory(@"c:\MarsRover");
                            Logger.Info($"Download started for date {datePhoto}");
                            webclient.DownloadFileTaskAsync(imageUri,
                            string.Format(@"c:\MarsRover\image{0}.jpg", Guid.NewGuid().ToString())).Wait();
                        }
                    }
                }
                else
                {
                   Logger.Error("Content is empty!");
                }
                Logger.Info($"Download Finished for date {datePhoto}");
            }
            else
            {
                Logger.Error("Not Valid date to download picture!");
            }
        }

        public string GetDate(string date)
        {
            try
            {
                var newDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
                return newDate.ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                Logger.Error($"Wrong date and time format! for {date}");
            }

            return string.Empty;
        }

        public Uri GetUriPath(string uriString, string datePhoto, string key)
        {
            var uri = new Uri(uriString);
            var pathBuilder = new UriBuilder(uri)
            {
                Path = $"photos?earth_date={datePhoto}&api_key={key}"
            };
            var uriPath = new Uri(uri, pathBuilder.Path);
            return uriPath;
        }

        public async Task<string> GetResponseAsync(Uri uri)
        {
            var url = uri?.AbsoluteUri;
            var client = new HttpClient();
            var urlDataString = Uri.UnescapeDataString(uri.ToString());

            Debug.WriteLine($"Get the image for (uri={urlDataString}");
            var response = await client.GetAsync(urlDataString) ??
                           throw new ArgumentNullException($"GetJsonResponseAsync({url})");

            // A non-OK response throws a web exception
            if (response.StatusCode != HttpStatusCode.OK) throw new WebException($"{response.StatusCode}");

            var requestResponse = string.Empty;
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                if (responseStream != null)
                    using (var reader = new StreamReader(responseStream))
                    {
                        requestResponse = reader.ReadToEnd();
                    }
            }

            return requestResponse;
        }
    }
}
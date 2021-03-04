using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MarsRover.Interfaces;
using MarsRover.Models;
using Newtonsoft.Json;
using NLog;

namespace MarsRover
{
    public class MarsRoverApiManager
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private string uristring ="https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/";

        public MarsRoverApiManager()
        {
            this.uristring = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/";
            
        }

        public async void GetPhotosAsync(string date, string key)
        {
            var datePhoto = GetDate(date);

            if (!string.IsNullOrEmpty(datePhoto))
            {
                var uri = new Uri(uristring);
                var pathBuilder = new UriBuilder(uri)
                {
                    Path = $"photos?earth_date={datePhoto}&api_key={key}"

                };
                var uriPath = new Uri(uri, pathBuilder.Path).ToString();
                var urlDataString = Uri.UnescapeDataString(uriPath);

                Logger.Trace($"Get the image for uri {urlDataString}");

                var client = new HttpClient();
                var response = client.GetAsync(urlDataString).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content =  await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(content))
                    {
                        var data = JsonConvert.DeserializeObject<Photos>(content);

                        foreach (var photo in data.photos)
                        {
                            var imageUri = new Uri(photo.Img_src);

                            using (WebClient webclient = new WebClient())
                            {
                                Directory.CreateDirectory(@"c:\MarsRover");
                                Logger.Info($"Download started for date {datePhoto}");
                                webclient.DownloadFileTaskAsync(imageUri, string.Format(@"c:\MarsRover\image{0}.jpg", Guid.NewGuid().ToString())).Wait();
                            }
                        }
                    }
                    else
                    {
                        Logger.Error("Content is empty!");
                    }
                }
                else
                {
                    Logger.Error("Internal server Error,Could not connect to server!");
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
                DateTime newDate = DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture);
                return newDate.ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
               Logger.Error($"Wrong date and time format! for {date}");
            }

           return String.Empty;
        }

        public async Task<string> GetJsonResponseAsync(Uri uri)
        {
            var url = uri?.AbsoluteUri;
            var client = new HttpClient();
            
            try
            {
                Debug.WriteLine($"GetJsonResponseAsync(url={url}");
                var response = await client.GetAsync(uri) ?? throw new ArgumentNullException($"GetJsonResponseAsync({url})");

                // A non-OK response throws a web exception
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new WebException($"{response.StatusCode}");
                }

                var requestResponse = string.Empty;
                using (var responseStream =  await response.Content.ReadAsStreamAsync())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            requestResponse = reader.ReadToEnd();
                        }
                    }
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                throw ;
            }
        }


    }
}

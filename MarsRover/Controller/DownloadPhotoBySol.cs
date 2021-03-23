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
using MarsRover.Services;

namespace MarsRover.Controller
{
   public class DownloadPhotoBySol : IDownloadPhoto
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IHttpCommClientService client = new HttpCommClientService();

        public DownloadPhotoBySol()
        {

        }

        public DownloadPhotoBySol(IHttpCommClientService client)
        {
            this.client = client ?? throw new ArgumentNullException(
                              nameof(client),
                              "Common client cannot be null."); ;
        }

        public async Task DownloadPhotoAsync(string date, string key, string uri)
        {
            var datePhoto = date;

            if (!string.IsNullOrEmpty(datePhoto))
            {
                var uriPath = GetUriPathSol(uri, datePhoto, key);

                var response = await this.client.GetResponseAsync(uriPath);

                if (!string.IsNullOrEmpty(response))
                {
                    var data = JsonConvert.DeserializeObject<Photos>(response);

                    foreach (var photo in data.photos)
                    {
                        var imageUri = new Uri(photo.Img_src);
                        using (var webclient = new WebClient())
                        {
                            var path = Directory.CreateDirectory(@"c:\MarsRover\" + datePhoto + @"\");
                            Logger.Info($"Download started for date {datePhoto}");
                            await webclient.DownloadFileTaskAsync(imageUri,
                            string.Format(path + "image{0}.jpg", Guid.NewGuid().ToString()));

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

        public Uri GetUriPathSol(string uriString, string datePhoto, string key)
        {
            var uri = new Uri(uriString);
            var pathBuilder = new UriBuilder(uri)
            {
                Path = $"photos?sol={datePhoto}&api_key={key}"
            };
            var uriPath = new Uri(uri, pathBuilder.Path);
            return uriPath;
        }       

    }
}

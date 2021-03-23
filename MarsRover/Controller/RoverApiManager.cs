using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarsRover.Controller;
using MarsRover.Interfaces;
using MarsRover.Models;
using MarsRover.Services;
using Newtonsoft.Json;
using NLog;

namespace MarsRover
{
    public class RoverApiManager : IRoverApiManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IDownloadPhoto downloadPhoto ;

        public RoverApiManager()
        {
            this.Uri = "https://api.nasa.gov/mars-photos/api/v1/rovers/";
            this.Rover = new Rover();            
        }

        public RoverApiManager(IDownloadPhoto downloadPhoto)
        {
            this.Uri = "https://api.nasa.gov/mars-photos/api/v1/rovers/";
            this.Rover = new Rover();
            this.downloadPhoto = downloadPhoto;

        }

        public string Uri { get; set; }

        public Rover Rover { get; set; }

        /// <summary>
        /// Download Photo for specific Rover By earth Date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <param name="rover"></param>
        public void DownloadPhotoForRoverAsync (string date, string key, Rover rover)
        {
            Logger.Info($"Download Photo For Rover {0}", Rover);
            var uri = UriRover(this.Uri, rover);
            DownloadPhoto dl = new DownloadPhoto(new HttpCommClientService ()) ;
            dl.DownloadPhotoAsync(date, key, uri).Wait();
        }

        /// <summary>
        /// Download Photo for specific Rover By Sol
        /// </summary>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <param name="rover"></param>
        public void DownloadPhotoForRoverBySol(string date, string key, Rover rover)
        {
            Logger.Info($"Download Photo For Rover by Sol {0}", Rover);
            var uri = UriRover(this.Uri, rover);
            var dl = new DownloadPhotoBySol();
            dl.DownloadPhotoAsync(date, key, uri).Wait();
        }

        /// <summary>
        /// Creates the URI path for Rover
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="rover"></param>
        /// <returns></returns>
        public string UriRover ( string uri, Rover rover)
        {
            return string.Concat(uri, rover.Name, "/");
        }     

    }
}
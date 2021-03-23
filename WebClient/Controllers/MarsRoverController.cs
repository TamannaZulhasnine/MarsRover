using System.Collections.Generic;
using MarsRover;
using MarsRover.Controller;
using Microsoft.AspNetCore.Mvc;
using WebClient.ViewModel;

namespace WebClient.Controllers
{
    public class MarsRoverController : Controller
    {
        public IActionResult Index()
        {
            // string date = "02 / 27 / 17";
            // return View (CreateViewModel( "02 / 27 / 17"));
           return View();
        }

        public ActionResult Download (string date = null, string url = null)
        {
            return View(CreateViewModel(date = "02 / 27 / 17", url));
        }

        public RoverPhotosViewModel CreateViewModel(string date, string url)
        {
            RoverPhotosViewModel roverPhotosViewModel = new RoverPhotosViewModel();
            roverPhotosViewModel.SelectedDate = date;
            var key = "DEMO_Key";
            var manager = new DownloadPhoto();
            manager.DownloadPhotoAsync(date, key, url).Wait();
            return roverPhotosViewModel;
        }

        public void DownloadPhotoByDate(string date, string url)
        {
            var key = "DEMO_Key";
            string dateT = "02 / 27 / 17";
            var manager = new DownloadPhoto();
            manager.DownloadPhotoAsync(dateT, key, url).Wait();
            
            
        }
    }
}
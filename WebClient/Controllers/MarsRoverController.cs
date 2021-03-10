using System.Collections.Generic;
using MarsRover;
using Microsoft.AspNetCore.Mvc;
using WebClient.ViewModel;

namespace WebClient.Controllers
{
    public class MarsRoverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CreateViewModel()
        {
            RoverPhotosViewModel roverPhotosViewModel = new RoverPhotosViewModel();
            roverPhotosViewModel.ListDate = new List<string>{"1", "2","3","4"};

            return View(roverPhotosViewModel);
        }

        public ActionResult DownloadPhotoByDate(string date)
        {
            var key = "DEMO_Key";
            string dateT = "02 / 27 / 17";
            var manager = new MarsRoverApiManager();
            manager.GetPhotosAsync(dateT, key);
            
            return null;
        }
    }
}
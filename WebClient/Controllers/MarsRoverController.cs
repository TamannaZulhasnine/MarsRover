using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
namespace WebClient.Controllers
{
    public class MarsRoverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

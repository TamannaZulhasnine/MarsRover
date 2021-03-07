using Microsoft.AspNetCore.Mvc;

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
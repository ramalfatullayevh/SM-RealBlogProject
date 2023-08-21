using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
    
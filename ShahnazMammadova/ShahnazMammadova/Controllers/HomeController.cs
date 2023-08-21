using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

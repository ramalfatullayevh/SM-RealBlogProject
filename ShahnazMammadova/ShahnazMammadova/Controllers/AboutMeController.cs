using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
	public class AboutMeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

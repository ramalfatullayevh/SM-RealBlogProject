using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
	public class StoriesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

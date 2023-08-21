using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
	public class BlogsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

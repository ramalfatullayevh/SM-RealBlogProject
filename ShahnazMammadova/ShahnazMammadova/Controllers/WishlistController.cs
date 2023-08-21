using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
	public class WishlistController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

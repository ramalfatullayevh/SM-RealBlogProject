using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
	public class ShopController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

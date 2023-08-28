using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
	
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

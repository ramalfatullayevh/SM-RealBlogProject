using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

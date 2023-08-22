using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
	[Area("shahnazm")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

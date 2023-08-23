using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
	[Area("shahnazm")]
	[Authorize(Roles = "SuperAdmin")]

	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

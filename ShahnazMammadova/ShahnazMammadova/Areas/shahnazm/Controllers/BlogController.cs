using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
    [Area("shahnazm")]
	[Authorize(Roles ="SuperAdmin")]
    public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		//[HttpGet]
  //      public IActionResult CreateBlog()
  //      {
  //          return View();
  //      }

		//[HttpPost]
  //      public IActionResult CreateBlog()
  //      {
  //          return View();
  //      }


  //      [HttpGet]
  //      public IActionResult EditBlog()
  //      {
  //          return View();
  //      }

  //      [HttpPost]
  //      public IActionResult EditBlog()
  //      {
  //          return View();
  //      }

  //      [HttpGet]
  //      public IActionResult DeleteBlog()
  //      {

  //      }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
    [Area("shahnazm")]
    public class StoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

        //[HttpGet]
        //public IActionResult CreateBlog()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CreateBlog()
        //{
        //    return View();
        //}


        //[HttpGet]
        //public IActionResult EditBlog()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult EditBlog()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult DeleteBlog()
        //{

        //}
    }
}

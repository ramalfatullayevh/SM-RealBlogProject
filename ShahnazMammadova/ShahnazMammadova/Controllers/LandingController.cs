using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Controllers
{
    public class LandingController : Controller
    {
        readonly AppDBContext _context;
      

		public LandingController(AppDBContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
        {
            LandingVM landing = new LandingVM
            {
                Blogs = await _context.Blogs.Include(b=>b.Category).Where(b => b.IsDeleted == false).Where(b => b.IsPopular == true).OrderByDescending(b => b.CreatedTime).ToListAsync(),
                Stories = await _context.Stories.Include(b=>b.Category).Where(b => b.IsDeleted == false).Where(b => b.IsPopular == true).OrderByDescending(b => b.CreatedTime).ToListAsync(),
            };
            return View(landing);
        }
	}
}
    
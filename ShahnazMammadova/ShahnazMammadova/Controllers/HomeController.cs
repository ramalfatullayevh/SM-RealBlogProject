using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;

namespace ShahnazMammadova.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.Include(s=>s.Category).Where(s=>s.IsDeleted == false).ToListAsync();  
            return View(sliders);
        }
    }
}

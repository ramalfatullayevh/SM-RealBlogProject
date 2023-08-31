using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
    [Area("shahnazm")]

	public class DashboardController : Controller
	{
		readonly AppDBContext _context;
        readonly UserManager<AppUser> _userManager;

        public DashboardController(AppDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = new DashboardVM
            {
                Blogs = await _context.Blogs.Where(x => !x.IsDeleted).ToListAsync(),
                Stories = await _context.Stories.Where(x => !x.IsDeleted).ToListAsync(),
                AppUsers = await _context.AppUser.ToListAsync(),
                UserRoles = new Dictionary<string, List<string>>()
            };

            foreach (var user in dashboard.AppUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                dashboard.UserRoles[user.UserName] = roles.ToList();
            }

            return View(dashboard);
        }

        public async Task<IActionResult> UserDelete(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _context.AppUser.FindAsync(id);
            if (user is null) return NotFound();

            _context.Remove(user);  
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));   
        }

    }
}

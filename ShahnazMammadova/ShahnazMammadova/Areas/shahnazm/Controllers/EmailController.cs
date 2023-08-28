using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
    
    public class EmailController : Controller
    {
        readonly AppDBContext _context;
        readonly UserManager<User> _userManager;
        public EmailController(AppDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AllMails()
        {
            return View();
        }
    }
}

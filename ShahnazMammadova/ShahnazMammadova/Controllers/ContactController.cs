using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Controllers
{
	public class ContactController : Controller
	{
		readonly AppDBContext _context;
		readonly UserManager<User> _userManager;
		public ContactController(AppDBContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(ContactVM vM)
		{
			var userid = _userManager.GetUserId(HttpContext.User);
			var user = await _userManager.FindByIdAsync(userid);
			var contact = new Contact();
			if (user is null) 
			{
				contact = new Contact
				{
					CreatedDate = DateTime.Now,	
					Name = vM.Name,
					Email = vM.Email,	
					Subject = vM.Subject,
					Message = vM.Message,	
				};
			}
			else 
			{
				contact = new Contact
				{
					CreatedDate = DateTime.Now,
					Name = user.Name,
					Email = user.Email,
					Subject = vM.Subject,
					Message = vM.Message,
				};
			}
			await _context.Contacts.AddAsync(contact);	
			await _context.SaveChangesAsync();
			ModelState.Clear();
			ViewBag.Message = "Mesajınız uğurla göndərildi";
			return View();
		}
	}
}

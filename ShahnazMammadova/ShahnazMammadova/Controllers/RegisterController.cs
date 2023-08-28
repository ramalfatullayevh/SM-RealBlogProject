using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShahnazMammadova.Helpers;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Controllers
{
	public class RegisterController : Controller
	{
		readonly UserManager<AppUser> _userManager;
		readonly SignInManager<AppUser> _signInManager;
		readonly RoleManager<IdentityRole> _roleManager;

		public RegisterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}


		//*****************Register Get*****************//
		public async Task<IActionResult> Index()
		{
			return View();
		}


		//*****************Register Post*****************//
		[HttpPost]
		public async Task<IActionResult> Index(RegisterVM registerVM)
		{
			var username = (registerVM.Name.ToLower().Trim() + registerVM.Surname.ToLower().Trim()).Trim();
			if (!ModelState.IsValid) return View(registerVM);
			var user = await _userManager.FindByNameAsync(username);
			if (user is not null)
			{
				var random = new Random();
				var randomnumber = random.Next(100000000, 999999999);
				username = username + randomnumber;
			}
			var email = await _userManager.FindByEmailAsync(registerVM.Email);
			if (email is not null)
			{
				ModelState.AddModelError("Email", "This Email address is already in use");
				return View(registerVM);	
			}
			user = new AppUser
			{
				Name = registerVM.Name,
				Surname = registerVM.Surname,	
				UserName = username,	
				Email = registerVM.Email,
				IsSubscribed = registerVM.IsSubscribed,	
			};
			var result = await _userManager.CreateAsync(user, registerVM.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}

				return View();
			}

			var role = await _userManager.AddToRoleAsync(user, "SuperAdmin");

			if (!role.Succeeded)
			{
				foreach (var item in role.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}

				return View();
			}

			await _signInManager.SignInAsync(user, true);
			return RedirectToAction("index", "login");
		}


		//*****************Add Role*****************//
		public async Task AddRoles()
		{
			foreach (var item in Enum.GetValues(typeof(Roles)))
			{
				if (!await _roleManager.RoleExistsAsync(item.ToString()))
				{
					await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });

				}
			}

		}
	}
}

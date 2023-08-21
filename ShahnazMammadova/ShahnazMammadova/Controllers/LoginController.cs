using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Controllers
{
	public class LoginController : Controller
	{
		readonly UserManager<User> _userManager;
		readonly SignInManager<User> _signInManager;

		public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


		//**************Login Get**************/
		public async Task<IActionResult> Index()
		{
			return View();
		}


		//**************Login Post**************/
		[HttpPost]
		public async Task<IActionResult> Index(LoginVM loginVM, string? ReturnUrl)
		{
			if (!ModelState.IsValid) return View(loginVM);
			var user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
				if (user == null)
				{
					ModelState.AddModelError("", "Login or Password is wrong");
					return View();
				}
			}

			var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.Rememberme, true);

			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "Login or Password is wrong");
				return View();
			}
			if (ReturnUrl == null)
			{
				return RedirectToAction("Index", "Home");

			}
			else
			{
				return Redirect(ReturnUrl);
			}
		}

	}
}

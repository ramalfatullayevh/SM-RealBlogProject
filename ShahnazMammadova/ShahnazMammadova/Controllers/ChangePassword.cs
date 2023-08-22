using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;
namespace ShahnazMammadova.Controllers
{
	public class ChangePassword : Controller
	{
		readonly UserManager<User> _usermanager;

		public ChangePassword(UserManager<User> usermanager)
		{
			_usermanager = usermanager;
		}

		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			if (!ModelState.IsValid) return View(model);	
			var user = await _usermanager.FindByEmailAsync(model.Email);
			var resetToken = await _usermanager.GeneratePasswordResetTokenAsync(user);
			var tokenlink = Url.Action("ResetPassword", "ChangePassword", new
			{
				userId = user.Id,
				token = resetToken
			}, HttpContext .Request.Scheme);

			MimeMessage message = new MimeMessage();
			MailboxAddress mailbox = new MailboxAddress("SuperAdmin", "tu7ma9clt@code.edu.az");
			message.From.Add(mailbox);
			MailboxAddress mailTo = new MailboxAddress("Member", model.Email);
			message.To.Add(mailTo);	

			var bodyBuilder = new BodyBuilder();
			bodyBuilder.TextBody = $"Reset Your Password: {tokenlink}";
			message.Body = bodyBuilder.ToMessageBody();	
			message.Subject = "Change Password Operation";

			SmtpClient client = new SmtpClient();
			client.Connect("smtp.gmail.com", 587, false);
			client.Authenticate("tu7ma9clt@code.edu.az", "annmioxvuiqhzkxz");
			client.SendAsync(message);

			ModelState.AddModelError("", "Check Your Email Address");
			return View();
		}



		public async Task<IActionResult> ResetPassword(string userid, string token)
		{
			TempData["userid"] = userid; 
			TempData["token"] = token;	
			return View();	
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			var userid = TempData["userid"];
			var token = TempData["token"];	
			if(userid is null || token is null)
			{
				ModelState.AddModelError("", "Something is wrong");
				return View();
			}
			var user = await _usermanager.FindByIdAsync(userid.ToString());
			var result =  await _usermanager.ResetPasswordAsync(user, token.ToString(), model.Password);
			if (result.Succeeded) return RedirectToAction("index", "login");
			return View();
		}
	}
}

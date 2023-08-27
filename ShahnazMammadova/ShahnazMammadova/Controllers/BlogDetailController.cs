using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;
using System.Web;

namespace ShahnazMammadova.Controllers
{
	public class BlogDetailController : Controller
	{
		readonly AppDBContext _context;
		readonly UserManager<User> _userManager;

		public BlogDetailController(AppDBContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(int? id)
		{
			if (id is null) return BadRequest();
			var blog = await _context.Blogs.Where(b => b.IsDeleted == false).Include(b => b.Category).Include(b => b.BlogComments).ThenInclude(b=>b.Replies).Include(b => b.BlogComments).ThenInclude(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
			if (blog is null) return NotFound();
			var lastPostId = await _context.Blogs.Where(b => b.IsDeleted == false).OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefaultAsync();
			ViewBag.LastPostId = lastPostId;

			return View(blog);
		}


		[HttpPost]
		public async Task<IActionResult> AddComment(BlogCommentVM review)
		{
			if (User.Identity.IsAuthenticated)
			{
				string userid =  _userManager.GetUserId(HttpContext.User);
				if (!ModelState.IsValid) return View(review);
				BlogComment comment = new BlogComment
				{
					UserId = userid,
					BlogId = review.BlogId,
					ReviewContent = review.ReviewContent,
					ReviewDate = DateTime.Now,
					Status = true,
				};
				await _context.BlogComments.AddAsync(comment);
				await _context.SaveChangesAsync();
				var formattedUrl = Url.Action("index", "blogdetail", new { id = review.BlogId });
				return Redirect(formattedUrl);
			}
			else
			{
				return RedirectToAction("index", "Login");
			}
		}
	}
}

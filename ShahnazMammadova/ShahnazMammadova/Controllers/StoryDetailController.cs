using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Controllers
{
	public class StoryDetailController : Controller
	{
		readonly AppDBContext _context;
		readonly UserManager<User> _userManager;

		public StoryDetailController(AppDBContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(int? id)
		{
			if (id is null) return BadRequest();
			var story = await _context.Stories.Where(b => b.IsDeleted == false).Include(b => b.Category).Include(b => b.StoryComments).ThenInclude(b => b.Replies).Include(b => b.StoryComments).ThenInclude(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
			if (story is null) return NotFound();
			var lastPostId = await _context.Stories.Where(b => b.IsDeleted == false).OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefaultAsync();
			ViewBag.LastPostId = lastPostId;
			return View(story);
		}

		[HttpPost]
		public async Task<IActionResult> AddComment(StoryCommentVM review)
		{
			if (User.Identity.IsAuthenticated)
			{
				string userid = _userManager.GetUserId(HttpContext.User);
				if (!ModelState.IsValid) return View(review);
				StoryComment comment = new StoryComment
				{
					UserId = userid,
					StoryId = review.StoryId,
					ReviewContent = review.ReviewContent,
					ReviewDate = DateTime.Now,
					Status = true,
				};
				await _context.StoryComments.AddAsync(comment);
				await _context.SaveChangesAsync();
				var formattedUrl = Url.Action("index", "storydetail", new { id = review.StoryId });
				return Redirect(formattedUrl);
			}
			else
			{
				return RedirectToAction("index", "Login");
			}
		}
	}
}

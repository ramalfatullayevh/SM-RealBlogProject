using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Controllers
{
	public class StoriesController : Controller
	{
		readonly AppDBContext _context;

		public StoriesController(AppDBContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(string? query, int page = 1)
		{
			var stories = new StoryVM
			{
				Stories = await _context.Stories.Where(b => b.IsDeleted == false).Include(b=>b.Category).OrderByDescending(b => b.ViewCount).ToListAsync(),
			};
			if (query is not null)
			{

				var search = stories.Stories.Where(c =>
					c.NameAz.ToLower().Trim().Contains(query.ToLower().Trim()) ||
					c.NameRu.ToLower().Trim().Contains(query.ToLower().Trim()) ||
					c.NameEng.ToLower().Trim().Contains(query.ToLower().Trim())
				).ToList();
				IEnumerable<Story> paginationsearch = search.Skip((page - 1) * 5).Take(5);
				PaginationVM<Story> searchpaginationVM = new PaginationVM<Story>
				{
					MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 5),
					CurrentPage = page,
					Items = paginationsearch,
					Query = query

				};

				return View(searchpaginationVM);
			}
			IEnumerable<Story> pagination = stories.Stories.Skip((page - 1) * 5).Take(5);
			PaginationVM<Story> paginationVM = new PaginationVM<Story>
			{
				MaxPageCount = (int)Math.Ceiling((decimal)stories.Stories.Count / 5),
				CurrentPage = page,
				Items = pagination
			};
			return View(paginationVM);
		}
	}
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Controllers
{
	public class BlogsController : Controller
	{
		readonly AppDBContext _context;

		public BlogsController(AppDBContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(string? query, int page = 1)
		{
			var blogs = new BlogVM
			{
				Blogs = await _context.Blogs.Where(b => b.IsDeleted == false).Include(b => b.Category).OrderByDescending(b => b.ViewCount).ToListAsync(),
			};
			if (query is not null)
			{

				var search = blogs.Blogs.Where(c =>
					c.NameAz.ToLower().Trim().Contains(query.ToLower().Trim()) ||
					c.NameRu.ToLower().Trim().Contains(query.ToLower().Trim()) ||
					c.NameEng.ToLower().Trim().Contains(query.ToLower().Trim())
				).ToList();
				IEnumerable<Blog> paginationsearch = search.Skip((page - 1) * 5).Take(5);
				PaginationVM<Blog> searchpaginationVM = new PaginationVM<Blog>
				{
					MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 5),
					CurrentPage = page,
					Items = paginationsearch,
					Query = query

				};


				return View(searchpaginationVM);
			}
			IEnumerable<Blog> pagination = blogs.Blogs.Skip((page - 1) * 5).Take(5);
			PaginationVM<Blog> paginationVM = new PaginationVM<Blog>
			{
				MaxPageCount = (int)Math.Ceiling((decimal)blogs.Blogs.Count / 5),
				CurrentPage = page,
				Items = pagination
			};
			return View(paginationVM);
		}
	}
}

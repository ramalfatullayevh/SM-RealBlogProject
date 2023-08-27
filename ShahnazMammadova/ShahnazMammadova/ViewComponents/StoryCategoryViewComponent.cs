using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;

namespace ShahnazMammadova.ViewComponents
{
	public class StoryCategoryViewComponent:ViewComponent
	{
		readonly AppDBContext _context;

		public StoryCategoryViewComponent(AppDBContext context)
		{
			_context = context;
		}


		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _context.Categories.Where(c=>c.IsDeleted == false).Include(c=>c.Stories.Where(b=>b.IsDeleted ==false).OrderByDescending(b=>b.ViewCount)).ToListAsync();	
			return View(categories);
		}
	}
}

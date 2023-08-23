using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
	[Area("shahnazm")]
	//[Authorize(Roles ="SuperAdmin")]
	public class CategoryController : Controller
	{
		readonly AppDBContext _context;

		//*****************Category Constructor*****************//
		public CategoryController(AppDBContext context)
		{
			_context = context;
		}


		//*****************Category GetAll*****************//
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var categories = new CatogryVM()
			{
				Categories = await _context.Categories.Include(ctg=>ctg.Blogs).Include(ctg=>ctg.Stories).ToListAsync(),	
			};

			return View(categories);
		}


		//*****************Category Create Get*****************//
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}


		//*****************Category Create Post*****************//
		[HttpPost]
		public async Task<IActionResult> Create(CreateCategoryVM create)
		{
			if(!ModelState.IsValid) return View(create);
			var category = new Category
			{
				CreatedTime = DateTime.Now,
				IsDeleted = false,
				NameAz = create.NameAz,
				NameRu = create.NameRu,	
				NameEng = create.NameEng,	
			};

			await _context.Categories.AddAsync(category);	
			await _context.SaveChangesAsync();	
			return RedirectToAction(nameof(Index));	
		}


		//*****************Category Update Get*****************//
		[HttpGet]
		public async Task<IActionResult> Update(int? id)
		{
			if(id is null) return BadRequest();
			var category = await _context.Categories.FindAsync(id);	
			if(category is null) return NotFound();
			var update = new UpdateCategoryVM
			{
				NameAz = category.NameAz,
				NameRu = category.NameRu,
				NameEng = category.NameEng,
			};
			return View(update);
		}


		//*****************Category Update Get*****************//
		[HttpPost]
		public async Task<IActionResult> Update(UpdateCategoryVM update, int? id)
		{
			if (!ModelState.IsValid) return View(update);
			if(id is null) return BadRequest();	
			var category = await _context.Categories.FindAsync(id);
			if (category is null) return NotFound();

			category.UpdatedTime = DateTime.Now;
			category.NameAz = update.NameAz;	
			category.NameRu = update.NameRu;	
			category.NameEng = update.NameEng;
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}


		//*****************Category Delete Get*****************//
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if(id is null) return BadRequest();	
			var category = await _context.Categories.FindAsync(id);	
			if (category is null) return NotFound();
			
			category.DeletedTime = DateTime.Now;	
			category.IsDeleted = true;	

			await _context.SaveChangesAsync();	
			return RedirectToAction(nameof(Index));	
		}
	}
}

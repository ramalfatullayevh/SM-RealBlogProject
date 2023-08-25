using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;
using ShahnazMammadova.Helpers;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
    [Area("shahnazm")]
	//[Authorize(Roles = "SuperAdmin")]

	public class StoryController : Controller
	{
        readonly AppDBContext _context;
        readonly IWebHostEnvironment _env;

        //*****************Story Controller*****************//
        public StoryController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        //*****************Story GetAll*****************//
        public async Task<IActionResult> Index()
        {
            var stories = await _context.Stories.Include(s=>s.Category).ToListAsync();  
            return View(stories);
        }


        //*****************Story Create Get*****************//
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg=>ctg.IsDeleted==false), nameof(Category.Id), nameof(Category.NameAz));
            return View();
        }


        //*****************Story Create Post*****************//
        [HttpPost]
        public async Task<IActionResult> Create(CreateStoryVM create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.Where(ctg=>ctg.IsDeleted==false), nameof(Category.Id), nameof(Category.NameAz));
                return View();
            }
            if (create.FirstImage != null)
            {
                string result = create.FirstImage.CheckValidate("image/", 1000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
            }
            if (create.SecondImage != null)
            {
                string result = create.SecondImage.CheckValidate("image/", 1000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));
                return View();
            }
            var story = new Story
            {
                CreatedTime = DateTime.Now, 
                IsDeleted = false,  
                NameAz = create.NameAz,
                NameRu = create.NameRu, 
                NameEng = create.NameEng,   
                DescriptionAz = create.DescriptionAz,   
                DescriptionRu = create.DescriptionRu,
                DescriptionEng = create.DescriptionEng,
                CategoryId = create.CategoryId, 
                IsPopular = create.IsPopular,   
                FirstImageUrl = create.FirstImage.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "storyimg")),
                SecondImageUrl = create.SecondImage?.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "storyimg")),
            };

            await _context.Stories.AddAsync(story);  
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //*****************Story Update Get*****************//
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            var story = await _context.Stories.FindAsync(id);
            if (story is null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));

            var update = new UpdateStoryVM
            {
                NameAz = story.NameAz,  
                NameEng = story.NameEng,    
                NameRu = story.NameRu,  
                DescriptionAz = story.DescriptionAz,
                DescriptionEng = story.DescriptionEng,
                DescriptionRu = story.DescriptionRu,    
                CategoryId = story.CategoryId,  
                IsPopular = story.IsPopular,    
                FirstImageUrl = story.FirstImageUrl,
                SecondImageUrl = story.SecondImageUrl,  
            };

            return View(update);
        }


        //*****************Story Update Post*****************//
        [HttpPost]
        public async Task<IActionResult> Update(UpdateStoryVM update, int? id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));
                return View(update);    
            }
            if (id is null) return BadRequest();
            var story = await _context.Stories.FindAsync(id);  
            if (story is null) return NotFound();   

            story.UpdatedTime = DateTime.Now;
            story.NameAz = update.NameAz;
            story.NameEng = update.NameEng;  
            story.NameRu = update.NameRu;
            story.DescriptionAz = update.DescriptionAz;
            story.DescriptionEng = update.DescriptionEng;
            story.DescriptionRu = update.DescriptionRu;
            story.CategoryId = update.CategoryId;    
            story.IsPopular = update.IsPopular;  

            if (update.FirstImage != null)
            {
                string result = update.FirstImage.CheckValidate("image/", 1000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }

                story.FirstImageUrl.DeleteFile(_env.WebRootPath, "user/assets/storyimg");
                story.FirstImageUrl = update.FirstImage.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "storyimg"));
            }

            if (update.SecondImage != null)
            {
                string result = update.SecondImage.CheckValidate("image/", 1000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }

                story.SecondImageUrl?.DeleteFile(_env.WebRootPath, "user/assets/storyimg");
                story.SecondImageUrl = update.SecondImage?.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "storyimg"));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //*****************Story Delete*****************//
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();  
            var story = await _context.Stories.FindAsync(id);   
            if (story is null) return NotFound();

            story.IsDeleted = true; 
            story.DeletedTime = DateTime.Now; 
            story.IsPopular = false;

            story.FirstImageUrl.DeleteFile(_env.WebRootPath, "user/assets/storyimg");
            story.SecondImageUrl?.DeleteFile(_env.WebRootPath, "user/assets/storyimg");


            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index)); 
        }
    }
}

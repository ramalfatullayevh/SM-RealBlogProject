using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Helpers;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
    [Area("shahnazm")]
	//[Authorize(Roles ="SuperAdmin")]
    public class BlogController : Controller
	{
        readonly AppDBContext _context;
        readonly IWebHostEnvironment _env;


        //*****************Story Constructor*****************//
        public BlogController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        //*****************Story GetAll*****************//
        public async Task<IActionResult> Index()
		{
            var blogs = await _context.Blogs.Include(b=>b.Category).ToListAsync(); 
			return View(blogs);
		}


        //*****************Story Create Get*****************//
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));

            return View();
        }


        //*****************Story Create Post*****************//
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVM create)
        {
            
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
            if (create.Video != null)
            {
                string result = create.Video.CheckValidate("video/", 10000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Video", result);
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));
                return View();
            }
            var blog = new Blog
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
                FirstImageUrl = create.FirstImage.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "blogimg")),
                SecondImageUrl = create.SecondImage.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "blogimg")),
                VideoUrl = create.Video.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "blogimg"))
            };

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //*****************Story Update Get*****************//
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog is null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));

            var update = new UpdateBlogVM
            {
                NameAz = blog.NameAz,
                NameEng = blog.NameEng,
                NameRu = blog.NameRu,
                DescriptionAz = blog.DescriptionAz,
                DescriptionEng = blog.DescriptionEng,
                DescriptionRu = blog.DescriptionRu,
                CategoryId = blog.CategoryId,
                IsPopular = blog.IsPopular,
                FirstImageUrl = blog.FirstImageUrl,
                SecondImageUrl = blog.SecondImageUrl,
                VideoUrl = blog.VideoUrl,   
            };

            return View(update);
        }


        //*****************Story Update Post*****************//
        [HttpPost]
        public async Task<IActionResult> Update(int? id , UpdateBlogVM update)
        {
            if (!ModelState.IsValid) return View(update);
            if (id is null) return BadRequest();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog is null) return NotFound();
            
            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));

            blog.UpdatedTime = DateTime.Now;
            blog.NameAz = update.NameAz;
            blog.NameEng = update.NameEng;
            blog.NameRu = update.NameRu;
            blog.DescriptionAz = update.DescriptionAz;
            blog.DescriptionEng = update.DescriptionEng;
            blog.DescriptionRu = update.DescriptionRu;
            blog.CategoryId = update.CategoryId;
            blog.IsPopular = update.IsPopular;

            if (update.FirstImage != null)
            {
                string result = update.FirstImage.CheckValidate("image/", 1000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }

                blog.FirstImageUrl.DeleteFile(_env.WebRootPath, "user/assets/blogimg");
                blog.FirstImageUrl = update.FirstImage.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "blogimg"));
            }

            if (update.SecondImage != null)
            {
                string result = update.SecondImage.CheckValidate("image/", 1000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }

                blog.SecondImageUrl.DeleteFile(_env.WebRootPath, "user/assets/blogimg");
                blog.SecondImageUrl = update.SecondImage.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "blogimg"));
            }

            if (update.Video != null)
            {
                string result = update.Video.CheckValidate("video/", 10000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Video", result);
                }

                blog.VideoUrl.DeleteFile(_env.WebRootPath, "user/assets/blogimg");
                blog.VideoUrl = update.Video.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "blogimg"));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //*****************Story Delete*****************//
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog is null) return NotFound();

            blog.IsDeleted = true;
            blog.DeletedTime = DateTime.Now;
            blog.IsPopular = false;

            blog.FirstImageUrl.DeleteFile(_env.WebRootPath, "user/assets/blogimg");
            blog.SecondImageUrl.DeleteFile(_env.WebRootPath, "user/assets/blogimg");
            blog.VideoUrl.DeleteFile(_env.WebRootPath, "user/assets/blogimg");


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

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
    public class SliderController : Controller
    {
        readonly AppDBContext _context;
        readonly IWebHostEnvironment _env;
        

        //*****************Slider Constructor*****************//
        public SliderController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        //*****************Slider Get All*****************//
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.Include(s=>s.Category).ToListAsync();
            return View(sliders);
        }


        //*****************Slider Create Get*****************//
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));

            return View();  
        }


        //*****************Slider Create Post*****************//
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM create)
        {
            
            if (create.Image != null)
            {
                string result = create.Image.CheckValidate("image/", 1000);
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

            var slider = new Slider
            {
                CreatedTime = DateTime.Now,
                IsDeleted = false,
                NameAz = create.NameAz,
                NameRu = create.NameRu,
                NameEng = create.NameEng,
                CategoryId = create.CategoryId,
                ImageUrl = create.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "sliderimg")),
                VideoUrl = create.Video.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "sliderimg"))
            };

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //*****************Slider Update Get*****************//
        public async Task<IActionResult> Update(int? id)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            var slider = await _context.Sliders.FindAsync(id);
            if (slider is null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));

            var update = new UpdateSliderVM
            {
                NameAz = slider.NameAz,
                NameEng = slider.NameEng,
                NameRu = slider.NameRu,
                CategoryId = slider.CategoryId,
                ImageUrl = slider.ImageUrl,
                VideoUrl = slider.VideoUrl,
            };
            return View(update);
        }


        //*****************Slider Update Post*****************//
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSliderVM update)
        {
            if (!ModelState.IsValid) return View(update);
            if (id is null) return BadRequest();
            var slider = await _context.Sliders.FindAsync(id);
            if (slider is null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories.Where(ctg => ctg.IsDeleted == false), nameof(Category.Id), nameof(Category.NameAz));

            slider.UpdatedTime = DateTime.Now;
            slider.NameAz = update.NameAz;
            slider.NameEng = update.NameEng;
            slider.NameRu = update.NameRu;
            slider.CategoryId = update.CategoryId;

            if (update.Image != null)
            {
                string result = update.Image.CheckValidate("image/", 1000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }

                slider.ImageUrl.DeleteFile(_env.WebRootPath, "user/assets/sliderimg");
                slider.ImageUrl = update.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "sliderimg"));
            }

            if (update.Video != null)
            {
                string result = update.Video.CheckValidate("video/", 10000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Video", result);
                }

                slider.VideoUrl.DeleteFile(_env.WebRootPath, "user/assets/sliderimg");
                slider.VideoUrl = update.Video.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "sliderimg"));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //*****************Slider Delete*****************//
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var slider = await _context.Sliders.FindAsync(id);
            if (slider is null) return NotFound();

            slider.IsDeleted = true;
            slider.DeletedTime = DateTime.Now;

            slider.ImageUrl.DeleteFile(_env.WebRootPath, "user/assets/sliderimg");
            slider.VideoUrl.DeleteFile(_env.WebRootPath, "user/assets/sliderimg");


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

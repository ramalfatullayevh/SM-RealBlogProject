using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
    public class UpdateSliderVM
    {
        public string NameAz { get; set; }
        public string NameEng { get; set; }
        public string NameRu { get; set; }

        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public IFormFile Image { get; set; }
        public IFormFile? Video { get; set; }
    }
}

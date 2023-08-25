using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
	public class CreateStoryVM
	{
        public string NameAz { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }
        public string DescriptionAz { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEng { get; set; }
        public string? FirstImageUrl { get; set; }
        public string? SecondImageUrl { get; set; }
        public bool IsPopular { get; set; }


        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public IFormFile FirstImage { get; set; }
        public IFormFile? SecondImage { get; set; }
    }
}

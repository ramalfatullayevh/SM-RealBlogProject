using ShahnazMammadova.Models.Base;

namespace ShahnazMammadova.Models
{
    public class Slider:BaseEntity
    {
        public string NameAz { get; set; }
        public string NameEng { get; set; }
        public string NameRu { get; set; }

        public string ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}

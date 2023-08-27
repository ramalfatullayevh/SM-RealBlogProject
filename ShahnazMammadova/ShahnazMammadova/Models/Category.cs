using ShahnazMammadova.Models.Base;

namespace ShahnazMammadova.Models
{
	public class Category:BaseEntity
	{
        public string NameAz { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }
		public int ViewCount { get; set; }


		public ICollection<Blog> Blogs { get; set; }
        public ICollection<Story> Stories { get; set; }
        public ICollection<Slider> Sliders { get; set; }
    }
}

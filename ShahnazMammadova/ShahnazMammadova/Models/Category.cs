using ShahnazMammadova.Models.Base;

namespace ShahnazMammadova.Models
{
	public class Category:BaseEntity
	{
        public string NameAz { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}

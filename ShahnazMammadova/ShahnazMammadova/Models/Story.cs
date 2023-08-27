using ShahnazMammadova.Models.Base;

namespace ShahnazMammadova.Models
{
    public class Story:BaseEntity
    {
        public string NameAz { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }
        public string DescriptionAz { get; set; } 
        public string DescriptionRu { get; set; } 
        public string DescriptionEng { get; set; }
        public string FirstImageUrl { get; set; }
        public string? SecondImageUrl { get; set; }
        public bool IsPopular { get; set; }
		public int ViewCount { get; set; }



		public int CategoryId { get; set; }
        public Category Category { get; set; }

		public ICollection<StoryComment> StoryComments { get; set; }

	}
}

using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
	public class StoryCommentVM
	{
		public string ReviewContent { get; set; }

		public int StoryId { get; set; }

		public Story? Story { get; set; }

		//public int? ParentCommentId { get; set; }

		//public BlogComment ParentComment { get; set; }

		//public List<BlogComment> Replies { get; set; }
	}
}

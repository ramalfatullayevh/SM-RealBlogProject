using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
	public class BlogCommentVM
	{
		public string ReviewContent { get; set; }

		public int BlogId { get; set; }

		public Blog? Blog { get; set; }

		public int? ParentCommentId { get; set; }

		public BlogComment ParentComment { get; set; }

		public List<BlogComment> Replies { get; set; }
	}
}

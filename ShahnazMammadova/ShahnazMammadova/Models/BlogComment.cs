using ShahnazMammadova.Models.Base;

namespace ShahnazMammadova.Models
{
	public class BlogComment
	{
        public int Id { get; set; }
        public string ReviewContent { get; set; }

		public DateTime ReviewDate { get; set; }

		public bool Status { get; set; }


		public string UserId { get; set; }

		public AppUser? User { get; set; }


		public int BlogId { get; set; }

		public Blog? Blog { get; set; }

		public int? ParentCommentId { get; set; }
		public BlogComment ParentComment { get; set; }
		public List<BlogComment> Replies { get; set; }

	}
}

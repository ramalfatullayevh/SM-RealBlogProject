using System.ComponentModel.DataAnnotations;

namespace ShahnazMammadova.ViewModels
{
	public class ContactVM
	{
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(15)]
		public string Name { get; set; }
		[Required, DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required, MinLength(5),MaxLength(250)]
		public string Subject { get; set; }
		[Required, MinLength(5), MaxLength(5000)]
		public string Message { get; set; }
		public DateTime CreatedDate { get; set; }

		public string? Reply { get; set; }
	}
}

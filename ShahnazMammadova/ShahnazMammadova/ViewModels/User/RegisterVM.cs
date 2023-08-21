using System.ComponentModel.DataAnnotations;

namespace ShahnazMammadova.ViewModels
{
	public class RegisterVM
	{
        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }
		[Required,MinLength(5), MaxLength(15)]
		public string Surname { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
		[Required,DataType(DataType.Password), Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
        public bool IsSubscribed { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace ShahnazMammadova.Models
{
	public class User:IdentityUser
	{
        public string Name { get; set; }			
        public string Surname { get; set; }	
        public bool IsSubscribed { get; set; }
        public string? ImageUrl { get; set; }
    }
}

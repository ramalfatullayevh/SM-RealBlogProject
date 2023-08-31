using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
	public class DashboardVM
	{
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Story> Stories { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public Dictionary<string, List<string>> UserRoles { get; set; }

    }
}

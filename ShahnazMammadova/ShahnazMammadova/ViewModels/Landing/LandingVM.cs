using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
    public class LandingVM
    {
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Story> Stories { get; set; }
    }
}

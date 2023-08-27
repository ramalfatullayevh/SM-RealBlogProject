using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.Models;

namespace ShahnazMammadova.DataAccessLayer.Context
{
	public class AppDBContext:IdentityDbContext<User>
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }	
		public DbSet<Category> Categories { get; set; }	
		public DbSet<Blog> Blogs { get; set; }	
		public DbSet<Story> Stories { get; set; }	
		public DbSet<Slider> Sliders { get; set; }	
		public DbSet<Contact> Contacts { get; set; }	
		public DbSet<BlogComment> BlogComments { get; set; }	


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}

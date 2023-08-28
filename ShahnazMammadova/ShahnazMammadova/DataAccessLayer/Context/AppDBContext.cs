using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.Models;

namespace ShahnazMammadova.DataAccessLayer.Context
{
	public class AppDBContext:IdentityDbContext<AppUser>
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

		public DbSet<AppUser> AppUser { get; set; }	
		public DbSet<Category> Categories { get; set; }	
		public DbSet<Blog> Blogs { get; set; }	
		public DbSet<Story> Stories { get; set; }	
		public DbSet<Slider> Sliders { get; set; }	
		public DbSet<Contact> Contacts { get; set; }	
		public DbSet<BlogComment> BlogComments { get; set; }	
		public DbSet<StoryComment> StoryComments { get; set; }	
		public DbSet<Mail> Mails { get; set; }	
		public DbSet<UserMail> UserMails { get; set; }	


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}

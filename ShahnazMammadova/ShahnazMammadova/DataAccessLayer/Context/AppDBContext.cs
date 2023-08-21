using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShahnazMammadova.Models;

namespace ShahnazMammadova.DataAccessLayer.Context
{
	public class AppDBContext:IdentityDbContext<User>
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }	


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}

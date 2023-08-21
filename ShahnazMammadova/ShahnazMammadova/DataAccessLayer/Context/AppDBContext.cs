using Microsoft.EntityFrameworkCore;

namespace ShahnazMammadova.DataAccessLayer.Context
{
	public class AppDBContext:DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}

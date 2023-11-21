using bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace bulky.Data
{
	public class ApplicationDbContext :DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> categories { get; set;  }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(new Category[]
			{
				new Category { Id = 1,Name="Action",Order=1},
				new Category { Id = 2,Name="Scifi",Order=2},
				new Category { Id = 3,Name="History",Order=3},

			});

			base.OnModelCreating(modelBuilder);

		}
	}
}

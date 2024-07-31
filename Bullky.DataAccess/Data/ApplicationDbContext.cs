using bulky.Models.Models;
using Bullky.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace bulky.DataAccess.Data
{
	public class ApplicationDbContext :DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> categories { get; set;  }
        public DbSet<Product> products { get; set; }
		public DbSet<Users> users { get; set; }
		public DbSet<Cart> carts { get; set; }
		public DbSet<CartItem> cartItems { get; set; }
		public DbSet<Order> orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Users>().HasData(new Users[]
			{ 
                new Users {Id=1,UserName="Hossam mostafa",Email="20201700226@cis.asu.edu.eg",Password="Hossam8#",PhoneNumber="01118506980",Ssn=30302080103752,CreditCardNum=7777777,Money=50000},
				new Users {Id=2,UserName="Hossam mostafa2",Email="20201700226@cis.asu.edu.eg",Password="Hossam8#",PhoneNumber="01118506980",Ssn=30302080103752,CreditCardNum=7777777,Money = 50000}


			});
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

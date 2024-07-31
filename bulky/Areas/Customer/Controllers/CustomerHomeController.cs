using bulky.DataAccess.Data;
using bulky.DataAccess.Migrations;
using Bullky.DataAccess.Repository.IRepository;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace bulky.Areas.Customer.Controllers
{
	public class CustomerHomeController : Controller
	{
		IUnitOfWork _UnitOfWork { get; set; }
		private readonly ApplicationDbContext _db;
		public CustomerHomeController(IUnitOfWork unitOfWork, ApplicationDbContext db)
		{
			_UnitOfWork = unitOfWork;
			_db = db;
			
		}
		public IActionResult Index(int userId)
		{
			IEnumerable<Product> ProductList =_UnitOfWork.Product.GetAll(includeproperties:"Category");
			var viewModel = new { ProductList = ProductList, UserId = userId };

			return View(viewModel);
		}

		public IActionResult Details(int productId, int userId)
		{
			Product product = _UnitOfWork.Product.Get(u=>u.Id == productId, includeproperties: "Category");
			var viewModel = new { product = product, UserId = userId, productId= productId };
			ViewBag.ProductId = productId;
			ViewBag.UserId = userId;
			return View(viewModel);
		}
		public IActionResult AddCart(int Quantity, int productId, int UserId)
		{
            
			Cart cart = Cart.GetInstance();
            cart=_db.carts.Include(c => c.Items).FirstOrDefault(u => u.UserId == UserId);

			if (cart == null)
			{

				cart = Cart.GetInstance();
				cart.UserId = UserId;
				cart.Items = new List<CartItem>();
				

				_db.carts.Add(cart);
				_db.SaveChanges(); 
			}

			// Retrieve the cart again to ensure that the Items collection is loaded
			cart = _db.carts.Include(c => c.Items).FirstOrDefault(u => u.UserId == UserId);

			if (cart != null)
			{
				CartItem item = new CartItem
				{
					Product = _db.products.FirstOrDefault(p => p.Id == productId),
					Quantity = Quantity
				};

				// Add the item to the cart
				_db.cartItems.Add(item);
				_db.SaveChanges();
				cart.Items.Add(item);
				_db.carts.Update(cart);
				// Save changes to the database
				_db.SaveChanges();
			}
			return NoContent();
		}
	}
}

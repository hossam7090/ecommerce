using bulky.DataAccess.Data;
using bulky.DataAccess.Migrations;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace bulky.Areas.Customer.Controllers
{
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CartController(ApplicationDbContext db)
		{
			_db = db;	
		}
		public IActionResult Index(int userId)
		{
			int total = 0;
			List<Product> cartItems = new List<Product>();
			List<int> Quantity = new List<int>();
			Cart cart = _db.carts.Include(c => c.Items).FirstOrDefault(u => u.UserId == userId);
            foreach(CartItem x in cart.Items)
			{
				Product product = _db.products.FirstOrDefault(p => p.Id == x.ProductId);

                cartItems.Add(product);
				Quantity.Add(x.Quantity);
				total += x.Quantity * ((int)product.Price);
            }
            var viewModel = new { cartItems = cartItems, Quantity = Quantity, total= total, userId= userId  };
            return View(viewModel);
		}
        public IActionResult DeleteFromCart(int userId, int productId)
        {
            // Find the cart
            Cart cart = _db.carts.Include(c => c.Items).FirstOrDefault(u => u.UserId == userId);

            // Find the cart item corresponding to the product
            CartItem cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem != null)
            {
                // Remove the item from the cart
                cart.Items.Remove(cartItem);
                _db.cartItems.Remove(cartItem); // Optionally, you can remove the item from the database as well

                // Update the total
                _db.SaveChanges();
            }

            return RedirectToAction("Index", new { userId = userId });
        }
        public IActionResult IncreaseQuantity(int userId, int productId)
        {
            UpdateCartItemQuantity(userId, productId, 1);
            return RedirectToAction("Index", new { userId = userId });
        }

        public IActionResult DecreaseQuantity(int userId, int productId)
        {
            UpdateCartItemQuantity(userId, productId, -1);
            return RedirectToAction("Index", new { userId = userId });
        }

        private void UpdateCartItemQuantity(int userId, int productId, int quantityChange)
        {
            Cart cart = _db.carts.Include(c => c.Items).FirstOrDefault(u => u.UserId == userId);
            CartItem cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem != null)
            {
                // Update the quantity
                cartItem.Quantity += quantityChange;

                // Ensure the quantity doesn't go below 1
                cartItem.Quantity = Math.Max(cartItem.Quantity, 1);

                // Update the total
                _db.SaveChanges();
            }
        }

        public IActionResult Order(int userId ,int total) {


			Order order=new Order();
			ViewData["Total"] = total;
			return View(order);
        }
        [HttpPost]
		public IActionResult Order(Order order ,int userId,int total)
        {
			Cart cart = _db.carts.Include(c => c.Items).FirstOrDefault(u => u.UserId == userId);
            order.Items = cart.Items;
			DateTime currentDate = DateTime.Now;
			order.DateTime = currentDate.AddDays(5);
            order.TotalPrice = total;
            Users user =_db.users.FirstOrDefault(u => u.Id == userId);
            if(user.CreditCardNum != 0) 
            {
                if (user.Money > order.TotalPrice)
                {
                    user.Money -= order.TotalPrice;
                    _db.Update(user);
                    _db.Add(order);
                    
                    foreach (var x in cart.Items)
                    {
                        _db.cartItems.Remove(x);
                    }
					cart.Items = null;
					_db.Update(cart);
					_db.SaveChanges();
                    TempData["success"] = "Category Added Successfully";
                    return RedirectToAction("Index", "CustomerHome", new { userId = userId });
                }
            }
			ModelState.AddModelError(string.Empty, "Something went wrong. Please try again later.");
			return View();
        }

	}
}

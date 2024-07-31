using bulky.DataAccess.Data;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Admin.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _db;
		public OrderController(ApplicationDbContext db) 
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<Order> orders=_db.orders.ToList();
			return View(orders);
		}
	}
}

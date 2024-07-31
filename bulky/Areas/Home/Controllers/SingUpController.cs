using bulky.DataAccess.Data;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Home.Controllers
{
	public class SingUpController : Controller
	{
		private readonly ApplicationDbContext _db;
		public SingUpController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			Users user =new Users();
			return View(user);
		}
		[HttpPost]
		public IActionResult Singup(Users user)
		{
			if(ModelState.IsValid)
			{
				_db.Add(user);
				_db.SaveChanges();
				TempData["success"] = "Singup Successfully";
				return RedirectToAction("Index","Home");
			}
			else
			{
				TempData["error"] = "Singup UnSuccessfully";
				return View("Index");
			}
		}
		}
}

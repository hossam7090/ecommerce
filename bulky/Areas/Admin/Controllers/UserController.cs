using bulky.DataAccess.Data;
using bulky.Models.Models;
using Bullky.DataAccess.Repository;
using Bullky.DataAccess.Repository.IRepository;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Admin.Controllers
{
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _db;
		public UserController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			var ObjUsersList = _db.users.ToList();
			return View(ObjUsersList);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Users? UserFromDb = _db.users.Find(id);
			if (UserFromDb == null)
			{
				return NotFound();
			}
			return View(UserFromDb);
		}

		[HttpPost]
		[ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			if (id == null || id == 0) { return NotFound(); }
			Users? obj = _db.users.Find(id); 
			if (obj == null) { return NotFound(); }
			_db.users.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully";
			return RedirectToAction("index");

		}
	}
}

using bulky.Data;
using bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
		{
			var ObjCategoriesList = _db.categories.ToList();
			return View(ObjCategoriesList);
		}
		public IActionResult Create() 
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if(obj.Name == obj.Order.ToString())
			{
				ModelState.AddModelError("name", "the dispaly order cannot exactly match the name");
			}
			if (obj.Name.ToLower() == "test")
			{
				ModelState.AddModelError("name", "test is invalid category name");
			}
			if (ModelState.IsValid)
			{
				_db.categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Category Added Successfully";
				return RedirectToAction();
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? CategoryFromDb =_db.categories.Find(id) ;
			if(CategoryFromDb == null)
			{
				return NotFound();
			}
			return View(CategoryFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.Order.ToString())
			{
				ModelState.AddModelError("name", "the dispaly order cannot exactly match the name");
			}
			if (obj.Name.ToLower() == "test")
			{
				ModelState.AddModelError("name", "test is invalid category name");
			}
			if (ModelState.IsValid)
			{
				_db.categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category Updated Successfully";

				return RedirectToAction("index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? CategoryFromDb = _db.categories.Find(id);
			if (CategoryFromDb == null)
			{
				return NotFound();
			}
			return View(CategoryFromDb);
		}

		[HttpPost]
		[ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			if(id == null || id == 0) { return NotFound(); }
			Category? obj = _db.categories.Find(id); 
			if (obj == null) { return NotFound(); }
			_db.categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully";
			return RedirectToAction("index");
			
		}

	}
}

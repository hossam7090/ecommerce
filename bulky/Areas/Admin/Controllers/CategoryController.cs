using bulky.DataAccess.Data;
using bulky.Models.Models;
using Bullky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Admin.Controllers
{
	public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            unitOfWork = db;
        }
        public IActionResult Index()
        {
            var ObjCategoriesList = unitOfWork.Category.GetAll().ToList();
            return View(ObjCategoriesList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
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
                unitOfWork.Category.Add(obj);
                unitOfWork.Save();
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
            Category? CategoryFromDb = unitOfWork.Category.Get(u => u.Id == id);
            if (CategoryFromDb == null)
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
                unitOfWork.Category.Update(obj);
                unitOfWork.Save();
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
            Category? CategoryFromDb = unitOfWork.Category.Get(u => u.Id == id);
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
            if (id == null || id == 0) { return NotFound(); }
            Category? obj = unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }
            unitOfWork.Category.Remove(obj);
            unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("index");

        }

    }
}

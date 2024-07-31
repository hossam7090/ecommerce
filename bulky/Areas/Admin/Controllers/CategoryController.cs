using bulky.Areas.Admin.Controllers.Command;
using bulky.DataAccess.Data;
using bulky.Models.Models;
using Bullky.DataAccess.Repository;
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
				var createCommand = new CreateCategoryCommand(unitOfWork, obj);
				ExecuteCommand(createCommand, "Category Added Successfully");
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
				var updateCommand = new UpdateCategoryCommand(unitOfWork, obj);
				ExecuteCommand(updateCommand, "Category Updated Successfully");

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
			var deleteCommand = new DeleteCategoryCommand(unitOfWork, obj);
			ExecuteCommand(deleteCommand, "Category Deleted Successfully");

			return RedirectToAction("index");

        }
		private void ExecuteCommand(ICommand command, string successMessage)
		{
			command.Execute();
			TempData["success"] = successMessage;
		}

	}
}

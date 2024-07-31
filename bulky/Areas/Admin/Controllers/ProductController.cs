using bulky.DataAccess.Data;
using bulky.Models.Models;
using Bullky.DataAccess.Repository.IRepository;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace bulky.Areas.Admin.Controllers
{
    //[Area("Customer")]
	public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
		private IWebHostEnvironment _webHostEnvironment;

		public ProductController(IUnitOfWork db,IWebHostEnvironment webHostEnvironment)
        {
            unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var ObjCategoriesList = unitOfWork.Product.GetAll(includeproperties:"Category").ToList();
            return View(ObjCategoriesList);
        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().
               Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString(),
               });
            ProductVM productVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product(),
            };

			if (id == null || id == 0)
			{
			    return View(productVM);
			}
            else
            {
				productVM.Product = unitOfWork.Product.Get(u=>u.Id == id);
				return View(productVM);
			}


		}

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM , IFormFile? file)
		{
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl)) {
                        var oldPath =Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create)){

                        file.CopyTo(fileStream);
                    }

					productVM.Product.ImageUrl = @"\images\product\" + fileName;
				}
                if(productVM.Product.Id==0)
                {
				unitOfWork.Product.Add(productVM.Product);

                }
                else
                {
                    unitOfWork.Product.Update(productVM.Product);
                }
                unitOfWork.Save();
                TempData["success"] = "Product Added Successfully";
                return RedirectToAction();
            }
            else
            {
				productVM.CategoryList = unitOfWork.Category.GetAll().
			   Select(u => new SelectListItem
			   {
				   Text = u.Name,
				   Value = u.Id.ToString(),
			   });
				
            return View(productVM);
			}
        }

       

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = unitOfWork.Product.Get(u => u.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            Product? obj = unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }
            unitOfWork.Product.Remove(obj);
            unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("index");

        }

    }
}
